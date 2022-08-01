namespace Koyomi.FSharp

open System

type Epoc =
    | Reiwa
    | Heisei
    | Showa

[<Struct>]
type Era =
    { Epoc: Epoc
      FullName: string
      Name: string
      Year: int }

type Koyomi =
    private
    | Holiday of DateTime * Era * string
    | Weekday of DateTime * Era

[<RequireQualifiedAccess>]
module Era =
    type private Range =
        | Current of DateTime
        | Recent of DateTime * DateTime

    let private name =
        function
        | Reiwa -> "令和"
        | Heisei -> "平成"
        | Showa -> "昭和"

    let private showa =
        let from = DateTime(1926, 12, 25)
        let until = DateTime(1989, 1, 7)
        Recent(from, until)

    let private heisei =
        let from = DateTime(1989, 1, 8)
        let until = DateTime(2019, 4, 30)
        Recent(from, until)

    let private reiwa =
        let from = DateTime(2019, 5, 1)
        Current(from)

    let private within (range: Range) (dt: DateTime) =
        match range with
        | Recent(from, until) -> from <= dt && dt <= until
        | Current(from) -> from <= dt

    let private year (dt: DateTime) (ep: Epoc) =
        match ep with
        | Reiwa -> dt.Year - 2018
        | Heisei -> dt.Year - 1988
        | Showa -> dt.Year - 1925

    let private fullName (dt: DateTime) (ep: Epoc) =
        let epocYear =
            match year dt ep with
            | 1 -> "元"
            | x -> string x
        let name' = name ep
        $"{name'}{epocYear}年"

    let from (dt: DateTime) =
        let epoc =
            if within reiwa dt then Reiwa
            else if within heisei dt then Heisei
            else if within showa dt then Showa
            else invalidArg (nameof dt) "昭和以前の元号は利用できません。"
        { Epoc = epoc
          FullName = fullName dt epoc
          Name = name epoc
          Year = year dt epoc }

// @see https://ja.wikipedia.org/wiki/元日
[<AutoOpen>]
module NewYearsDay =
    [<Literal>]
    let private NAME = "元日"

    let private (|Established|Expired|) (dt: DateTime) =
        match dt.Year with
        | y when 1948 < y -> Established
        | _ -> Expired

    let private (|NewYearsDay|_|) (dt: DateTime) =
        match (dt.Month, dt.Day) with
        | (1, 1) -> Some()
        | _ -> None

    let newYearsDay (dt: DateTime) =
        match dt with
        | Established & NewYearsDay -> Ok NAME
        | _ -> Error dt

// @see https://ja.wikipedia.org/wiki/成人の日
[<AutoOpen>]
module ComingOfAgeDay =
    [<Literal>]
    let private NAME = "成人の日"

    let private (|Established|Amended|Expired|) (dt: DateTime) =
        match dt.Year with
        | y when 1948 < y && y < 2000 -> Established
        | y when 2000 <= y -> Amended
        | _ -> Expired

    let private (|ComingOfAgeDay|_|) (dt: DateTime) =
        match (dt.Month, dt.Day) with
        | (1, 15) -> Some()
        | _ -> None

    let private (|HappyMonday|_|) (dt: DateTime) =
        match (dt.Month, dt.DayOfWeek) with
        | (1, DayOfWeek.Monday) ->
            match dt.Day with
            | d when 8 <= d && d <= 14 -> Some()
            | _ -> None
        | _ -> None

    let comingOfAgeDay (dt: DateTime) =
        match dt with
        | Established & ComingOfAgeDay -> Ok NAME
        | Amended & HappyMonday -> Ok NAME
        | _ -> Error dt

// @see https://ja.wikipedia.org/wiki/建国記念の日
[<AutoOpen>]
module NationalFoundationDay =
    [<Literal>]
    let private NAME = "建国記念の日"

    let private (|Established|Expired|) (dt: DateTime) =
        match dt.Year with
        | y when 1967 <= y -> Established
        | _ -> Expired

    let private (|NationalFoundationDay|_|) (dt: DateTime) =
        match (dt.Month, dt.Day) with
        | (2, 11) -> Some()
        | _ -> None

    let nationalFoundationDay (dt: DateTime) =
        match dt with
        | Established & NationalFoundationDay -> Ok NAME
        | _ -> Error dt

// @see https://ja.wikipedia.org/wiki/天皇誕生日
[<AutoOpen>]
module EmperorsBirthday =
    [<Literal>]
    let private NAME = "天皇誕生日"

    let private (|EmperorsBirthday|_|) (dt: DateTime) =
        let era = Era.from dt
        match (era.Epoc, dt.Month, dt.Day) with
        | (Showa, 4, 29) when 1948 < dt.Year -> Some()
        | (Heisei, 12, 23) -> Some()
        | (Reiwa, 2, 23) -> Some()
        | _ -> None

    let emperorsBirthday (dt: DateTime) =
        match dt with
        | EmperorsBirthday -> Ok NAME
        | _ -> Error dt

// @see https://ja.wikipedia.org/wiki/春分の日
[<AutoOpen>]
module VernalEquinoxDay =
    [<Literal>]
    let private NAME = "春分の日"

    let private (|Established|Expired|) (dt: DateTime) =
        match dt.Year with
        | y when 1948 < y -> Established
        | _ -> Expired

    let private (|VernalEquinoxDay|_|) (dt: DateTime) =
        let vernalEquinoxDay' (year: int) =
            let x = float (year - 1980)
            let y = ((0.242194 * x) + 20.8431) |> Math.Floor |> int
            let z = (x / 4.0) |> Math.Floor |> int
            y - z

        match (dt.Month, dt.Day) with
        | (3, d) when d = vernalEquinoxDay' dt.Year -> Some()
        | _ -> None

    let vernalEquinoxDay (dt: DateTime) =
        match dt with
        | Established & VernalEquinoxDay -> Ok NAME
        | _ -> Error dt

[<RequireQualifiedAccess>]
module Koyomi =
    let private either (f1: 'a -> 'c) (f2: 'b -> 'c) =
        function
        | Ok x -> f1 x
        | Error x -> f2 x

    let from (dt: DateTime) =
        let holiday =
            newYearsDay
            >> either Ok comingOfAgeDay
            >> either Ok nationalFoundationDay
            >> either Ok emperorsBirthday
            >> either Ok vernalEquinoxDay
        match holiday dt with
        | Ok holiday -> Holiday (dt, Era.from dt, holiday)
        | Error _ -> Weekday (dt, Era.from dt)

    let holiday (k: Koyomi) =
        match k with
        | Holiday (_, _, h) -> Some h
        | _ -> None

    let isHoliday (k: Koyomi)  =
        match k with
        | Holiday _ -> true
        | Weekday _ -> false
