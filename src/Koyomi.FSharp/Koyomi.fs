module Koyomi.FSharp

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

type private HappyMonday =
    | Second
    | Third

let private happyMonday (w: HappyMonday) (dt: DateTime) =
    match dt.DayOfWeek with
    | DayOfWeek.Monday ->
        match (w, dt.Day) with
        | (Second, d) when 8 <= d && d <= 14 -> true
        | (Third, d) when 15 <= d && d <= 21 -> true
        | _ -> false
    | _ -> false

type private Equinox =
    | Vernal
    | Autumnal

let private equinoxDay (e: Equinox) (year: int) =
    let equinox =
        match e with
        | Vernal -> 20.8431
        | Autumnal -> 23.2488
    let x = float (year - 1980)
    let y = ((0.242194 * x) + equinox) |> Math.Floor |> int
    let z = (x / 4.0) |> Math.Floor |> int
    y - z

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
        match dt.Month with
        | 1 -> if happyMonday Second dt then Some() else None
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

    let private vernalEquinox = equinoxDay Vernal

    let private (|Established|Expired|) (dt: DateTime) =
        match dt.Year with
        | y when 1948 < y -> Established
        | _ -> Expired

    let private (|VernalEquinoxDay|_|) (dt: DateTime) =
        match (dt.Month, dt.Day) with
        | (3, d) when d = vernalEquinox dt.Year -> Some()
        | _ -> None

    let vernalEquinoxDay (dt: DateTime) =
        match dt with
        | Established & VernalEquinoxDay -> Ok NAME
        | _ -> Error dt

// @see https://ja.wikipedia.org/wiki/みどりの日
[<AutoOpen>]
module GreenDay =
    [<Literal>]
    let private NAME = "みどりの日"

    let private (|Established|Amended|Expired|) (dt: DateTime) =
        match dt.Year with
        | y when 1989 <= y && y <= 2006 -> Established
        | y when 2007 <= y -> Amended
        | _ -> Expired

    let private (|BeforeAmended|_|) (dt: DateTime) =
        match (dt.Month, dt.Day) with
        | (4, 29) -> Some()
        | _ -> None

    let private (|AfterAmended|_|) (dt: DateTime) =
        match (dt.Month, dt.Day) with
        | (5, 4) -> Some()
        | _ -> None

    let greenDay (dt: DateTime) =
        match dt with
        | Established & BeforeAmended -> Ok NAME
        | Amended & AfterAmended -> Ok NAME
        | _ -> Error dt

// @see https://ja.wikipedia.org/wiki/昭和の日
[<AutoOpen>]
module ShowaDay =
    [<Literal>]
    let private NAME = "昭和の日"

    let private (|Established|Expired|) (dt: DateTime) =
        match dt.Year with
        | y when 2007 <= y -> Established
        | _ -> Expired

    let private (|ShowaDay|_|) (dt: DateTime) =
        match (dt.Month, dt.Day) with
        | (4, 29) -> Some()
        | _ -> None

    let showaDay (dt: DateTime) =
        match dt with
        | Established & ShowaDay -> Ok NAME
        | _ -> Error dt

// @see https://ja.wikipedia.org/wiki/憲法記念日_(日本)
[<AutoOpen>]
module ConstitutionDay =
    [<Literal>]
    let private NAME = "憲法記念日"

    let private (|Established|Expired|) (dt: DateTime) =
        match (dt.Year) with
        | y when 1948 <= y -> Established
        | _ -> Expired

    let private (|ConstitutionDay|_|) (dt: DateTime) =
        match (dt.Month, dt.Day) with
        | (5, 3) -> Some()
        | _ -> None

    let constitutionDay (dt: DateTime) =
        match dt with
        | Established & ConstitutionDay -> Ok NAME
        | _ -> Error dt

// @see https://ja.wikipedia.org/wiki/こどもの日
[<AutoOpen>]
module ChildrensDay =
    [<Literal>]
    let private NAME = "こどもの日"

    let private (|Established|Expired|) (dt: DateTime) =
        match (dt.Year) with
        | y when 1948 <= y -> Established
        | _ -> Expired

    let private (|ChildrensDay|_|) (dt: DateTime) =
        match (dt.Month, dt.Day) with
        | (5, 5) -> Some()
        | _ -> None

    let childrensDay (dt: DateTime) =
        match dt with
        | Established & ChildrensDay -> Ok NAME
        | _ -> Error dt

// @see https://ja.wikipedia.org/wiki/こどもの日
[<AutoOpen>]
module MarineDay =
    [<Literal>]
    let private NAME = "海の日"

    let private (|Established|Amended|Spot|Expired|) (dt: DateTime) =
        match dt.Year with
        | 2020 -> Spot
        | 2021 -> Spot
        | y when 1996 <= y && y <= 2002 -> Established
        | y when 2003 <= y -> Amended
        | _ -> Expired

    let private (|MarineDay|_|) (dt: DateTime) =
        match (dt.Month, dt.Day) with
        | (7, 20) -> Some()
        | _ -> None

    let private (|HappyMonday|_|) (dt: DateTime) =
        match dt.Month with
        | 7 -> if happyMonday Third dt then Some() else None
        | _ -> None

    let private (|BeforeOlympic|_|) (dt: DateTime) =
        match (dt.Month, dt.Day) with
        | (7, 23) -> Some()
        | _ -> None

    let private (|TokyoOlympic|_|) (dt: DateTime) =
        match (dt.Month, dt.Day) with
        | (7, 22) -> Some()
        | _ -> None

    let marineDay (dt: DateTime) =
        match dt with
        | Established & MarineDay -> Ok NAME
        | Amended & HappyMonday -> Ok NAME
        | Spot & BeforeOlympic -> Ok NAME
        | Spot & TokyoOlympic -> Ok NAME
        | _ -> Error dt

// @see https://ja.wikipedia.org/wiki/海の日
[<AutoOpen>]
module MountainDay =
    [<Literal>]
    let private NAME = "山の日"

    let private (|Established|Spot|Expired|) (dt: DateTime) =
        match dt.Year with
        | 2020 -> Spot
        | 2021 -> Spot
        | y when 2016 <= y -> Established
        | _ -> Expired

    let private (|MountainDay|_|) (dt: DateTime) =
        match (dt.Month, dt.Day) with
        | (8, 11) -> Some()
        | _ -> None

    let private (|BeforeOlympic|_|) (dt: DateTime) =
        match (dt.Month, dt.Day) with
        | (8, 10) -> Some()
        | _ -> None

    let private (|TokyoOlympic|_|) (dt: DateTime) =
        match (dt.Month, dt.Day) with
        | (8, 8) -> Some()
        | _ -> None

    let mountainDay (dt: DateTime) =
        match dt with
        | Established & MountainDay -> Ok NAME
        | Spot & BeforeOlympic -> Ok NAME
        | Spot & TokyoOlympic -> Ok NAME
        | _ -> Error dt

// @see https://ja.wikipedia.org/wiki/敬老の日
[<AutoOpen>]
module RespectForTheAgedDay =
    [<Literal>]
    let private NAME = "敬老の日"

    let private (|Established|Amended|Expired|) (dt: DateTime) =
        match dt.Year with
        | y when 1966 <= y && y <= 2002 -> Established
        | y when 2003 <= y -> Amended
        | _ -> Expired

    let private (|RespectForTheAgedDay|_|) (dt: DateTime) =
        match (dt.Month, dt.Day) with
        | (9, 15) -> Some()
        | _ -> None

    let private (|HappyMonday|_|) (dt: DateTime) =
        match dt.Month with
        | 9 -> if happyMonday Third dt then Some() else None
        | _ -> None

    let respectForTheAgedDay (dt: DateTime) =
        match dt with
        | Established & RespectForTheAgedDay -> Ok NAME
        | Amended & HappyMonday -> Ok NAME
        | _ -> Error dt

// @see https://ja.wikipedia.org/wiki/秋分の日
[<AutoOpen>]
module AutumnalEquinoxDay =
    [<Literal>]
    let private NAME = "秋分の日"

    let private autumnalEquinox = equinoxDay Autumnal

    let private (|Established|Expired|) (dt: DateTime) =
        match dt.Year with
        | y when 1948 < y -> Established
        | _ -> Expired

    let private (|AutumnalEquinoxDay|_|) (dt: DateTime) =
        match (dt.Month, dt.Day) with
        | (9, d) when d = autumnalEquinox dt.Year -> Some()
        | _ -> None

    let autumnalEquinoxDay (dt: DateTime) =
        match dt with
        | Established & AutumnalEquinoxDay -> Ok NAME
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
            >> either Ok greenDay
            >> either Ok showaDay
            >> either Ok constitutionDay
            >> either Ok childrensDay
            >> either Ok marineDay
            >> either Ok mountainDay
            >> either Ok respectForTheAgedDay
            >> either Ok autumnalEquinoxDay

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
