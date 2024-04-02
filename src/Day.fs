namespace Koyomi.FSharp

open System

type JapaneseHoliday =
    | AutumnalEquinoxDay
    | ChildrensDay
    | ComingOfAgeDay
    | ConstitutionDay
    | CultureDay
    | EmperorsBirthday
    | EnthronmentCeremonyOfEmperorHeisei
    | EnthronmentCeremonyOfEmperorReiwa
    | EnthronmentOfEmperorReiwa
    | GreenDay
    | LaborThanksgivingDay
    | MarineDay
    | MountainDay
    | MouringCeremonyOfEmperorShowa
    | NationalFoundationDay
    | NewYearsDay
    | PhysicalEducationDay
    | RespectForTheAgeDay
    | ShowaDay
    | SportsDay
    | SubstituteDay
    | VernalEquinoxDay
    | WeddingCeremonyOfPrinceAkihito
    | WeddingCeremonyOfPrinceNaruhito

[<RequireQualifiedAccess>]
module JapaneseHoliday =
    let private equinoxDay (equinox: double) (year: int) =
        let x = double (year - 1980)
        let y = Math.Floor((0.242194 * x) + equinox)
        let z = Math.Floor(x / 4.0)
        y - z |> abs |> int

    let inline private autumnalEquinoxDay' (date: DateOnly) = date.Day = equinoxDay 23.2488 date.Year

    let inline private vernalEquinoxDay' (date: DateOnly) = date.Day = equinoxDay 20.8431 date.Year

    let inline private happyMondaySecond (date: DateOnly) =
        match (date.DayOfWeek, date.Day) with
        | (DayOfWeek.Monday, d) when 8 <= d && d <= 14 -> true
        | _ -> false

    let inline private happyMondayThird (date: DateOnly) =
        match (date.DayOfWeek, date.Day) with
        | (DayOfWeek.Monday, d) when 15 <= d && d <= 21 -> true
        | _ -> false

    let autumnalEquinoxDay (date: DateOnly) =
        match (date.Year, date.Month) with
        | (y, 9) when 1949 <= y ->
            if autumnalEquinoxDay' date then
                Some(AutumnalEquinoxDay)
            else
                None
        | _ -> None

    let childrensDay (date: DateOnly) =
        match (date.Year, date.Month, date.Day) with
        | (y, 5, 5) when 1948 <= y -> Some(ChildrensDay)
        | _ -> None

    let comingOfAgeDay (date: DateOnly) =
        match (date.Year, date.Month, date.Day) with
        | (y, 1, 15) when 1949 <= y && y <= 1999 -> Some(ComingOfAgeDay)
        | (y, 1, _) when 2000 <= y ->
            if happyMondaySecond date then
                Some(ComingOfAgeDay)
            else
                None
        | _ -> None

    let constitutionDay (date: DateOnly) =
        match (date.Year, date.Month, date.Day) with
        | (y, 5, 3) when 1948 <= y -> Some(ConstitutionDay)
        | _ -> None

    let cultureDay (date: DateOnly) =
        match (date.Year, date.Month, date.Day) with
        | (y, 11, 3) when 1948 <= y -> Some(CultureDay)
        | _ -> None

    let emperorsBirthday (date: DateOnly) =
        match (date.Year, date.Month, date.Day) with
        | (y, 4, 29) when 1949 <= y && y <= 1988 -> Some(EmperorsBirthday)
        | (y, 12, 23) when 1989 <= y && y <= 2018 -> Some(EmperorsBirthday)
        | (y, 2, 23) when 2020 <= y -> Some(EmperorsBirthday)
        | _ -> None

    let greenDay (date: DateOnly) =
        match (date.Year, date.Month, date.Day) with
        | (y, 4, 29) when 1989 <= y && y <= 2006 -> Some(GreenDay)
        | (y, 5, 4) when 2007 <= y -> Some(GreenDay)
        | _ -> None

    let holidayName (jh: JapaneseHoliday) =
        match jh with
        | AutumnalEquinoxDay -> "秋分の日"
        | ChildrensDay -> "こどもの日"
        | ComingOfAgeDay -> "成人の日"
        | ConstitutionDay -> "憲法記念日"
        | CultureDay -> "文化の日"
        | EmperorsBirthday -> "天皇誕生日"
        | EnthronmentCeremonyOfEmperorHeisei -> "即位礼正殿の儀"
        | EnthronmentCeremonyOfEmperorReiwa -> "即位礼正殿の儀"
        | EnthronmentOfEmperorReiwa -> "天皇即位"
        | GreenDay -> "みどりの日"
        | LaborThanksgivingDay -> "勤労感謝の日"
        | MarineDay -> "海の日"
        | MountainDay -> "山の日"
        | MouringCeremonyOfEmperorShowa -> "昭和天皇大喪の礼"
        | NationalFoundationDay -> "建国記念の日"
        | NewYearsDay -> "元日"
        | PhysicalEducationDay -> "体育の日"
        | RespectForTheAgeDay -> "敬老の日"
        | ShowaDay -> "昭和の日"
        | SportsDay -> "スポーツの日"
        | SubstituteDay -> "振替休日"
        | VernalEquinoxDay -> "春分の日"
        | WeddingCeremonyOfPrinceAkihito -> "明仁親王の結婚の儀"
        | WeddingCeremonyOfPrinceNaruhito -> "徳仁親王の結婚の儀"

    let imperialCeremonyDay (date: DateOnly) =
        match (date.Year, date.Month, date.Day) with
        | (1959, 4, 10) -> Some(WeddingCeremonyOfPrinceAkihito)
        | (1989, 2, 24) -> Some(MouringCeremonyOfEmperorShowa)
        | (1990, 11, 12) -> Some(EnthronmentCeremonyOfEmperorHeisei)
        | (1993, 6, 9) -> Some(WeddingCeremonyOfPrinceNaruhito)
        | (2019, 5, 1) -> Some(EnthronmentOfEmperorReiwa)
        | (2019, 10, 22) -> Some(EnthronmentCeremonyOfEmperorReiwa)
        | _ -> None

    let laborThanksgivingDay (date: DateOnly) =
        match (date.Year, date.Month, date.Day) with
        | (y, 11, 23) when 1948 <= y -> Some(LaborThanksgivingDay)
        | _ -> None

    let marineDay (date: DateOnly) =
        match (date.Year, date.Month, date.Day) with
        | (2020, 7, 23) -> Some(MarineDay)
        | (2021, 7, 22) -> Some(MarineDay)
        | (y, 7, 20) when 1996 <= y && y <= 2002 -> Some(MarineDay)
        | (y, 7, _) when 2003 <= y -> if happyMondayThird date then Some(MarineDay) else None
        | _ -> None

    let mountainDay (date: DateOnly) =
        match (date.Year, date.Month, date.Day) with
        | (2020, 8, 10) -> Some(MountainDay)
        | (2021, 8, 8) -> Some(MountainDay)
        | (y, 8, 11) when 2016 <= y -> Some(MountainDay)
        | _ -> None

    let nationalFoundationDay (date: DateOnly) =
        match (date.Year, date.Month, date.Day) with
        | (y, 2, 11) when 1967 <= y -> Some(NationalFoundationDay)
        | _ -> None

    let newYearsDay (date: DateOnly) =
        match (date.Year, date.Month, date.Day) with
        | (y, 1, 1) when 1949 <= y -> Some(NewYearsDay)
        | _ -> None

    let physicalEducationDay (date: DateOnly) =
        match (date.Year, date.Month, date.Day) with
        | (y, 10, 10) when 1966 <= y && y <= 1999 -> Some(PhysicalEducationDay)
        | (y, 10, _) when 2000 <= y && y <= 2019 ->
            if happyMondaySecond date then
                Some(PhysicalEducationDay)
            else
                None
        | _ -> None

    let respectForTheAgeDay (date: DateOnly) =
        match (date.Year, date.Month, date.Day) with
        | (y, 9, 15) when 1966 <= y && y <= 2002 -> Some(RespectForTheAgeDay)
        | (y, 9, _) when 2003 <= y ->
            if happyMondayThird date then
                Some(RespectForTheAgeDay)
            else
                None
        | _ -> None

    let showaDay (date: DateOnly) =
        match (date.Year, date.Month, date.Day) with
        | (y, 4, 29) when 2007 <= y -> Some(ShowaDay)
        | _ -> None

    let sportsDay (date: DateOnly) =
        match (date.Year, date.Month, date.Day) with
        | (2020, 7, 24) -> Some(SportsDay)
        | (2021, 7, 23) -> Some(SportsDay)
        | (y, 10, _) when 2020 <= y -> if happyMondaySecond date then Some(SportsDay) else None
        | _ -> None

    let vernalEquinoxDay (date: DateOnly) =
        match (date.Year, date.Month) with
        | (y, 3) when 1949 <= y ->
            if vernalEquinoxDay' date then
                Some(VernalEquinoxDay)
            else
                None
        | _ -> None

    let inline private holidayWithoutSubstitute (date: DateOnly) =
        autumnalEquinoxDay date
        |> Option.orElse (childrensDay date)
        |> Option.orElse (comingOfAgeDay date)
        |> Option.orElse (constitutionDay date)
        |> Option.orElse (cultureDay date)
        |> Option.orElse (emperorsBirthday date)
        |> Option.orElse (greenDay date)
        |> Option.orElse (imperialCeremonyDay date)
        |> Option.orElse (laborThanksgivingDay date)
        |> Option.orElse (marineDay date)
        |> Option.orElse (mountainDay date)
        |> Option.orElse (nationalFoundationDay date)
        |> Option.orElse (newYearsDay date)
        |> Option.orElse (physicalEducationDay date)
        |> Option.orElse (respectForTheAgeDay date)
        |> Option.orElse (showaDay date)
        |> Option.orElse (sportsDay date)
        |> Option.orElse (vernalEquinoxDay date)

    let rec substituteHoliday (date: DateOnly) =
        if date < new DateOnly(1973, 4, 30) || date.Year <= 1973 then
            None
        else
            let pred = date.AddDays(-1)

            match holidayWithoutSubstitute pred with
            | None -> None
            | Some(_) when pred.DayOfWeek = DayOfWeek.Sunday -> Some(SubstituteDay)
            | _ -> substituteHoliday pred

    let hoilday (date: DateOnly) =
        holidayWithoutSubstitute date |> Option.orElse (substituteHoliday date)

type JapaneseWeekday =
    | Getsu
    | Ka
    | Sui
    | Moku
    | Kin
    | Do
    | Nichi

[<RequireQualifiedAccess>]
module JapaneseWeekday =
    let private weekdays = [| "月"; "火"; "水"; "木"; "金"; "土"; "日" |]

    let fromNumber (number: int) =
        match number with
        | 1 -> Some(Getsu)
        | 2 -> Some(Ka)
        | 3 -> Some(Sui)
        | 4 -> Some(Moku)
        | 5 -> Some(Kin)
        | 6 -> Some(Do)
        | 7 -> Some(Nichi)
        | _ -> None

    let fromName (name: string) =
        weekdays
        |> Array.tryFindIndex (fun n -> n = name)
        |> Option.bind (fun i -> fromNumber (i + 1))

    let number (weekday: JapaneseWeekday) =
        match weekday with
        | Getsu -> 1
        | Ka -> 2
        | Sui -> 3
        | Moku -> 4
        | Kin -> 5
        | Do -> 6
        | Nichi -> 7

    let name (weekday: JapaneseWeekday) = weekdays[number weekday - 1]

    let fromDateOnly (date: DateOnly) =
        let i =
            match date.DayOfWeek with
            | DayOfWeek.Sunday -> 7
            | w -> int w

        fromNumber i |> Option.get
