module Koyomi.FSharp

open System

type Epoc =
    | Reiwa
    | Heisei
    | Showa

type Era =
    { Epoc: Epoc
      FullName: string
      Name: string
      Year: int }

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
        | (Second, d) -> 8 <= d && d <= 14
        | (Third, d) -> 15 <= d && d <= 21
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

/// @see https://ja.wikipedia.org/wiki/元日
[<RequireQualifiedAccess>]
module NewYearsDay =
    [<Literal>]
    let private NAME = "元日"

    let private (|Enforced|_|) (dt: DateTime) =
        if 1948 < dt.Year then Some () else None

    let private (|NewYearsDay|_|) (dt: DateTime) =
        match (dt.Month, dt.Day) with
        | (1, 1) -> Some NAME
        | _ -> None

    let orNot (dt: DateTime) =
        match dt with
        | Enforced & NewYearsDay name -> Ok name
        | _ -> Error dt

/// @see https://ja.wikipedia.org/wiki/成人の日
[<RequireQualifiedAccess>]
module ComingOfAgeDay =
    [<Literal>]
    let private NAME = "成人の日"

    let private happyMonday' = happyMonday Second

    let private (|Enforced|Amended|OutOfRange|) (dt: DateTime) =
        match dt.Year with
        | y when 1948 < y && y < 2000 -> Enforced
        | y when 2000 <= y -> Amended
        | _ -> OutOfRange

    let private (|ComingOfAgeDay|_|) (dt: DateTime) =
        match (dt.Month, dt.Day) with
        | (1, 15) -> Some NAME
        | _ -> None

    let private (|HappyMonday|_|) (dt: DateTime) =
        match (dt.Month, happyMonday' dt) with
        | (1, true) -> Some NAME
        | _ -> None

    let orNot (dt: DateTime) =
        match dt with
        | Enforced & ComingOfAgeDay name -> Ok name
        | Amended & HappyMonday name -> Ok name
        | _ -> Error dt

/// @see https://ja.wikipedia.org/wiki/建国記念の日
[<RequireQualifiedAccess>]
module NationalFoundationDay =
    [<Literal>]
    let private NAME = "建国記念の日"

    let private (|Enforced|_|) (dt: DateTime) =
        if 1967 <= dt.Year then Some () else None

    let private (|NationalFoundationDay|_|) (dt: DateTime) =
        match (dt.Month, dt.Day) with
        | (2, 11) -> Some NAME
        | _ -> None

    let orNot (dt: DateTime) =
        match dt with
        | Enforced & NationalFoundationDay name -> Ok name
        | _ -> Error dt

// @see https://ja.wikipedia.org/wiki/天皇誕生日
[<RequireQualifiedAccess>]
module EmperorsBirthday =
    [<Literal>]
    let private NAME = "天皇誕生日"

    let private (|EmperorsBirthday|_|) (dt: DateTime) =
        let era = Era.from dt
        match (era.Epoc, dt.Month, dt.Day) with
        | (Showa, 4, 29) when 1948 < dt.Year -> Some NAME
        | (Heisei, 12, 23) -> Some NAME
        | (Reiwa, 2, 23) -> Some NAME
        | _ -> None

    let orNot (dt: DateTime) =
        match dt with
        | EmperorsBirthday name -> Ok name
        | _ -> Error dt

/// @see https://ja.wikipedia.org/wiki/春分の日
[<RequireQualifiedAccess>]
module VernalEquinoxDay =
    [<Literal>]
    let private NAME = "春分の日"

    let private vernalEquinox = equinoxDay Vernal

    let private (|Enforced|_|) (dt: DateTime) =
        if 1948 < dt.Year then Some () else None

    let private (|VernalEquinoxDay|_|) (dt: DateTime) =
        match (dt.Month, dt.Day = vernalEquinox dt.Year) with
        | (3, true) -> Some NAME
        | _ -> None

    let orNot (dt: DateTime) =
        match dt with
        | Enforced & VernalEquinoxDay name -> Ok name
        | _ -> Error dt

/// @see https://ja.wikipedia.org/wiki/みどりの日
[<RequireQualifiedAccess>]
module GreenDay =
    [<Literal>]
    let private NAME = "みどりの日"

    let private (|Enforced|Amended|OutOfRange|) (dt: DateTime) =
        match dt.Year with
        | y when 1989 <= y && y <= 2006 -> Enforced
        | y when 2007 <= y -> Amended
        | _ -> OutOfRange

    let private (|UntilAmended|_|) (dt: DateTime) =
        match (dt.Month, dt.Day) with
        | (4, 29) -> Some NAME
        | _ -> None

    let private (|SinceAmended|_|) (dt: DateTime) =
        match (dt.Month, dt.Day) with
        | (5, 4) -> Some NAME
        | _ -> None

    let orNot (dt: DateTime) =
        match dt with
        | Enforced & UntilAmended name -> Ok name
        | Amended & SinceAmended name -> Ok name
        | _ -> Error dt

/// @see https://ja.wikipedia.org/wiki/昭和の日
[<RequireQualifiedAccess>]
module ShowaDay =
    [<Literal>]
    let private NAME = "昭和の日"

    let private (|Enforced|_|) (dt: DateTime) =
        if 2007 <= dt.Year then Some () else None

    let private (|ShowaDay|_|) (dt: DateTime) =
        match (dt.Month, dt.Day) with
        | (4, 29) -> Some NAME
        | _ -> None

    let orNot (dt: DateTime) =
        match dt with
        | Enforced & ShowaDay name -> Ok name
        | _ -> Error dt

/// @see https://ja.wikipedia.org/wiki/憲法記念日_(日本)
[<RequireQualifiedAccess>]
module ConstitutionDay =
    [<Literal>]
    let private NAME = "憲法記念日"

    let private (|Enforced|_|) (dt: DateTime) =
        match dt.Year with
        | y when 1948 <= y -> Some ()
        | _ -> None

    let private (|ConstitutionDay|_|) (dt: DateTime) =
        match (dt.Month, dt.Day) with
        | (5, 3) -> Some NAME
        | _ -> None

    let orNot (dt: DateTime) =
        match dt with
        | Enforced & ConstitutionDay name -> Ok name
        | _ -> Error dt

/// @see https://ja.wikipedia.org/wiki/こどもの日
[<RequireQualifiedAccess>]
module ChildrensDay =
    [<Literal>]
    let private NAME = "こどもの日"

    let private (|Enforced|_|) (dt: DateTime) =
        if 1948 <= dt.Year then Some () else None

    let private (|ChildrensDay|_|) (dt: DateTime) =
        match (dt.Month, dt.Day) with
        | (5, 5) -> Some NAME
        | _ -> None

    let orNot (dt: DateTime) =
        match dt with
        | Enforced & ChildrensDay name -> Ok name
        | _ -> Error dt

/// @see https://ja.wikipedia.org/wiki/こどもの日
[<RequireQualifiedAccess>]
module MarineDay =
    [<Literal>]
    let private NAME = "海の日"

    let private happyMonday' = happyMonday Third

    let private (|Enforced|Amended|Spot|OutOfRange|) (dt: DateTime) =
        match dt.Year with
        | 2020 -> Spot
        | 2021 -> Spot
        | y when 1996 <= y && y <= 2002 -> Enforced
        | y when 2003 <= y -> Amended
        | _ -> OutOfRange

    let private (|MarineDay|_|) (dt: DateTime) =
        match (dt.Month, dt.Day) with
        | (7, 20) -> Some NAME
        | _ -> None

    let private (|HappyMonday|_|) (dt: DateTime) =
        match (dt.Month, happyMonday' dt) with
        | (7, true) -> Some NAME
        | _ -> None

    let private (|BeforeOlympic|_|) (dt: DateTime) =
        match (dt.Month, dt.Day) with
        | (7, 23) -> Some NAME
        | _ -> None

    let private (|TokyoOlympic|_|) (dt: DateTime) =
        match (dt.Month, dt.Day) with
        | (7, 22) -> Some NAME
        | _ -> None

    let orNot (dt: DateTime) =
        match dt with
        | Enforced & MarineDay name -> Ok name
        | Amended & HappyMonday name -> Ok name
        | Spot & BeforeOlympic name -> Ok name
        | Spot & TokyoOlympic name -> Ok name
        | _ -> Error dt

/// @see https://ja.wikipedia.org/wiki/海の日
[<RequireQualifiedAccess>]
module MountainDay =
    [<Literal>]
    let private NAME = "山の日"

    let private (|Enforced|Spot|OutOfRange|) (dt: DateTime) =
        match dt.Year with
        | 2020 -> Spot
        | 2021 -> Spot
        | y when 2016 <= y -> Enforced
        | _ -> OutOfRange

    let private (|MountainDay|_|) (dt: DateTime) =
        match (dt.Month, dt.Day) with
        | (8, 11) -> Some NAME
        | _ -> None

    let private (|BeforeOlympic|_|) (dt: DateTime) =
        match (dt.Month, dt.Day) with
        | (8, 10) -> Some NAME
        | _ -> None

    let private (|TokyoOlympic|_|) (dt: DateTime) =
        match (dt.Month, dt.Day) with
        | (8, 8) -> Some NAME
        | _ -> None

    let orNot (dt: DateTime) =
        match dt with
        | Enforced & MountainDay name -> Ok name
        | Spot & BeforeOlympic name -> Ok name
        | Spot & TokyoOlympic name -> Ok name
        | _ -> Error dt

/// @see https://ja.wikipedia.org/wiki/敬老の日
[<RequireQualifiedAccess>]
module RespectForTheAgedDay =
    [<Literal>]
    let private NAME = "敬老の日"

    let private happyMonday' = happyMonday Third

    let private (|Enforced|Amended|OutOfRange|) (dt: DateTime) =
        match dt.Year with
        | y when 1966 <= y && y <= 2002 -> Enforced
        | y when 2003 <= y -> Amended
        | _ -> OutOfRange

    let private (|RespectForTheAgedDay|_|) (dt: DateTime) =
        match (dt.Month, dt.Day) with
        | (9, 15) -> Some NAME
        | _ -> None

    let private (|HappyMonday|_|) (dt: DateTime) =
        match (dt.Month, happyMonday' dt) with
        | (9, true) -> Some NAME
        | _ -> None

    let orNot (dt: DateTime) =
        match dt with
        | Enforced & RespectForTheAgedDay name -> Ok name
        | Amended & HappyMonday name -> Ok name
        | _ -> Error dt

/// @see https://ja.wikipedia.org/wiki/秋分の日
[<RequireQualifiedAccess>]
module AutumnalEquinoxDay =
    [<Literal>]
    let private NAME = "秋分の日"

    let private autumnalEquinox = equinoxDay Autumnal

    let private (|Enforced|_|) (dt: DateTime) =
        if 1948 < dt.Year then Some () else None

    let private (|AutumnalEquinoxDay|_|) (dt: DateTime) =
        match (dt.Month, dt.Day = autumnalEquinox dt.Year) with
        | (9, true) -> Some NAME
        | _ -> None

    let orNot (dt: DateTime) =
        match dt with
        | Enforced & AutumnalEquinoxDay name -> Ok name
        | _ -> Error dt

/// @see https://ja.wikipedia.org/wiki/スポーツの日_(日本)
[<RequireQualifiedAccess>]
module PhysicalEducationDay =
    [<Literal>]
    let private NAME = "体育の日"

    let private happyMonday' = happyMonday Second

    let private (|Enforced|Amended|OutOfRange|) (dt: DateTime) =
        match dt.Year with
        | y when 1966 <= y && y <= 1999 -> Enforced
        | y when 2000 <= y && y <= 2019 -> Amended
        | _ -> OutOfRange

    let private (|PhysicalEducationDay|_|) (dt: DateTime) =
        match (dt.Month, dt.Day) with
        | (10, 10) -> Some NAME
        | _ -> None

    let private (|HappyMonday|_|) (dt: DateTime) =
        match (dt.Month, happyMonday' dt) with
        | (10, true) -> Some NAME
        | _ -> None

    let orNot (dt: DateTime) =
        match dt with
        | Enforced & PhysicalEducationDay name -> Ok name
        | Amended & HappyMonday name -> Ok name
        | _ -> Error dt

/// @see https://ja.wikipedia.org/wiki/スポーツの日_(日本)
[<RequireQualifiedAccess>]
module SportsDay =
    [<Literal>]
    let private NAME = "スポーツの日"

    let private happyMonday' = happyMonday Second

    let private (|Enforced|Spot|OutOfRange|) (dt: DateTime) =
        match dt.Year with
        | 2020 -> Spot
        | 2021 -> Spot
        | y when 2020 <= y -> Enforced
        | _ -> OutOfRange

    let private (|BeforeOlympic|_|) (dt: DateTime) =
        match (dt.Month, dt.Day) with
        | (7, 24) -> Some NAME
        | _ -> None

    let private (|TokyoOlympic|_|) (dt: DateTime) =
        match (dt.Month, dt.Day) with
        | (7, 23) -> Some NAME
        | _ -> None

    let private (|SportsDay|_|) (dt: DateTime) =
        match (dt.Month, happyMonday' dt) with
        | (10, true) -> Some NAME
        | _ -> None

    let orNot (dt: DateTime) =
        match dt with
        | Enforced & SportsDay name -> Ok name
        | Spot & BeforeOlympic name -> Ok name
        | Spot & TokyoOlympic name -> Ok name
        | _ -> Error dt

/// @see https://ja.wikipedia.org/wiki/文化の日
[<RequireQualifiedAccess>]
module CultureDay =
    [<Literal>]
    let private NAME = "文化の日"

    let private (|Enforced|_|) (dt: DateTime) =
        if 1948 <= dt.Year then Some () else None

    let private (|CultureDay|_|) (dt: DateTime) =
        match (dt.Month, dt.Day) with
        | (11, 3) -> Some NAME
        | _ -> None

    let orNot (dt: DateTime) =
        match dt with
        | Enforced & CultureDay name -> Ok name
        | _ -> Error dt

/// @see https://ja.wikipedia.org/wiki/勤労感謝の日
[<RequireQualifiedAccess>]
module LaborThanksgivingDay =
    [<Literal>]
    let NAME = "勤労感謝の日"

    let private (|Enforced|_|) (dt: DateTime) =
        if 1948 <= dt.Year then Some () else None

    let private (|LaborThanksgivingDay|_|) (dt: DateTime) =
        match (dt.Month, dt.Day) with
        | (11, 23) -> Some NAME
        | _ -> None

    let orNot (dt: DateTime) =
        match dt with
        | Enforced & LaborThanksgivingDay name -> Ok name
        | _ -> Error dt

/// @see https://ja.wikipedia.org/wiki/国民の祝日
[<RequireQualifiedAccess>]
module ImperialCeremony =
    let orNot (dt: DateTime) =
        match (dt.Year, dt.Month, dt.Day) with
        | (1959, 4, 10) -> Ok "明仁親王の結婚の儀"
        | (1989, 2, 24) -> Ok "昭和天皇大喪の礼"
        | (1990, 11, 12) -> Ok "即位礼正殿の儀"
        | (1993, 6, 9) -> Ok "徳仁親王の結婚の儀"
        | (2019, 5, 1) -> Ok "令和天皇即位"
        | (2019, 10, 22) -> Ok "即位礼正殿の儀"
        | _ -> Error dt

let private either (f1: 'a -> 'c) (f2: 'b -> 'c) =
    function
    | Ok x -> f1 x
    | Error x -> f2 x

let private holiday =
    NewYearsDay.orNot
    >> either Ok ComingOfAgeDay.orNot
    >> either Ok NationalFoundationDay.orNot
    >> either Ok EmperorsBirthday.orNot
    >> either Ok VernalEquinoxDay.orNot
    >> either Ok GreenDay.orNot
    >> either Ok ShowaDay.orNot
    >> either Ok ConstitutionDay.orNot
    >> either Ok ChildrensDay.orNot
    >> either Ok MarineDay.orNot
    >> either Ok MountainDay.orNot
    >> either Ok RespectForTheAgedDay.orNot
    >> either Ok AutumnalEquinoxDay.orNot
    >> either Ok PhysicalEducationDay.orNot
    >> either Ok SportsDay.orNot
    >> either Ok CultureDay.orNot
    >> either Ok LaborThanksgivingDay.orNot

/// @see https://ja.wikipedia.org/wiki/振替休日
[<RequireQualifiedAccess>]
module Substitute =
    [<Literal>]
    let private NAME = "振替休日"

    let private enforced = DateTime(1973, 4, 30)

    let rec private substituteHoliday (y: DateTime) =
        match holiday y with
        | Ok _ ->
            if y.DayOfWeek = DayOfWeek.Sunday
            then Ok NAME
            else y.AddDays(-1) |> substituteHoliday
        | x -> x

    let orNot (dt: DateTime) =
        if dt < enforced
        then Error dt
        else dt.AddDays(-1) |> substituteHoliday

type Koyomi =
    private
    | Holiday of DateTime * Era * string
    | Weekday of DateTime * Era

[<RequireQualifiedAccess>]
module Koyomi =
    let private holidayAndSubstitute =
        holiday >> either Ok Substitute.orNot

    let private dateTime =
        function
        | Holiday (dt, _, _) -> dt
        | Weekday (dt, _) -> dt

    let from (dt: DateTime) =
        match holidayAndSubstitute dt with
        | Ok h -> Holiday (dt, Era.from dt, h)
        | Error _ -> Weekday (dt, Era.from dt)

    let init (y: int) (m: int) (d: int) =
        DateTime(y, m, d) |> from

    let holiday =
        function
        | Holiday (_, _, h) -> Some h
        | _ -> None

    let isHoliday =
        function
        | Holiday _ -> true
        | Weekday _ -> false

    let era =
        function
        | Holiday (_, e, _) -> e
        | Weekday (_, e) -> e

    let year (k: Koyomi) = (dateTime k).Year

    let month (k: Koyomi) = (dateTime k).Month

    let day (k: Koyomi) = (dateTime k).Day

    let dayOfWeek (k: Koyomi) = (dateTime k).DayOfWeek
