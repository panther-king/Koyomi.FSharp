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

[<Literal>]
let private NEW_YEARS_DAY = "元日"

[<Literal>]
let private COMING_OF_AGE_DAY = "成人の日"

[<Literal>]
let private NATIONAL_FOUNDATION_DAY = "建国記念の日"

[<Literal>]
let private EMPERORS_BIRTHDAY = "天皇誕生日"

[<Literal>]
let private VERNAL_EQUINOX_DAY = "春分の日"

let inline private isMonday (dt: DateTime) =
    dt.DayOfWeek = DayOfWeek.Monday

module private January =
    // @see https://ja.wikipedia.org/wiki/成人の日
    let inline private comingOfAgeDay (dt: DateTime) =
        match (dt.Year, dt.Day) with
        | (y, d) when 2000 <= y ->
            if  (8 <= d && d <=14) && isMonday dt then Some COMING_OF_AGE_DAY
            else None
        | (y, d) when (1948 < y && y < 2000) ->
            if d = 15 then Some COMING_OF_AGE_DAY
            else None
        | _ -> None

    // @see https://ja.wikipedia.org/wiki/元日
    let inline private newYear'sDay (dt: DateTime) =
        match (dt.Year, dt.Day) with
        | (y, 1) when 1948 < y -> Some NEW_YEARS_DAY
        | _ -> None

    let holiday (dt: DateTime) =
        comingOfAgeDay dt
        |> Option.orElse (newYear'sDay dt)

module private February =
    // @see https://ja.wikipedia.org/wiki/建国記念の日
    let inline private nationalFoundationDay (dt: DateTime) =
        match (dt.Year, dt.Day) with
        | (y, 11) when 1967 <= y -> Some NATIONAL_FOUNDATION_DAY
        | _ -> None

    // @see https://ja.wikipedia.org/wiki/天皇誕生日
    let inline private reiwaEmperor'sBirthday (dt: DateTime) =
        match (dt.Year, dt.Day) with
        | (y, 23) when y <= 2020 -> Some EMPERORS_BIRTHDAY
        | _ -> None

    let holiday (dt: DateTime) =
        nationalFoundationDay dt
        |> Option.orElse (reiwaEmperor'sBirthday dt)

module private March =
    // @see https://ja.wikipedia.org/wiki/春分の日
    let vernalEquinoxDay (dt: DateTime) =
        let vernalEquinoxDay' year =
            let x = year - 1980
            let y = int ((0.242194 * float x) + 20.8431)
            let z = int (float x / 4.0)
            y - z

        match (dt.Year, dt.Day) with
        | (y, d) when 1948 < y ->
            if d = vernalEquinoxDay' y then Some VERNAL_EQUINOX_DAY
            else None
        | _ -> None

[<RequireQualifiedAccess>]
module Koyomi =
    let from (dt: DateTime) =
        let holiday =
            match dt.Month with
            | 1 -> January.holiday dt
            | 2 -> February.holiday dt
            | _ -> failwith "Something wrong"
        match holiday with
        | Some holiday -> Holiday (dt, Era.from dt, holiday)
        | None -> Weekday (dt, Era.from dt)

    let holiday (k: Koyomi) =
        match k with
        | Holiday (_, _, h) -> Some h
        | _ -> None

    let isHoliday (k: Koyomi)  =
        match k with
        | Holiday _ -> true
        | Weekday _ -> false
