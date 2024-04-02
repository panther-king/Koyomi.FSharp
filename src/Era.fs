namespace Koyomi.FSharp

open System

type JapaneseEra =
    | Reiwa of int
    | Heisei of int
    | Showa of int
    | Taisho of int
    | Meiji of int

[<RequireQualifiedAccess>]
module JapaneseEra =
    let inline private heisei (date: DateOnly) =
        let from = DateOnly(1989, 1, 8)
        let until = DateOnly(2019, 4, 30)

        match from <= date && date <= until with
        | false -> None
        | true ->
            let b = date.Year + 12
            let y = if b < 2000 then 1900 else 2000
            Some(Heisei(b - y))

    let inline private meiji (date: DateOnly) =
        let from = DateOnly(1868, 10, 23)
        let until = DateOnly(1912, 7, 29)

        match from <= date && date <= until with
        | false -> None
        | true -> Some(Meiji(date.Year + 33 - 1900))

    let inline private reiwa (date: DateOnly) =
        let from = DateOnly(2019, 5, 1)

        match from <= date with
        | false -> None
        | true -> Some(Reiwa(date.Year - 18 - 2000))

    let inline private showa (date: DateOnly) =
        let from = DateOnly(1926, 12, 25)
        let until = DateOnly(1989, 1, 7)

        match from <= date && date <= until with
        | false -> None
        | true -> Some(Showa(date.Year - 25 - 1900))

    let inline private taisho (date: DateOnly) =
        let from = DateOnly(1912, 7, 30)
        let until = DateOnly(1926, 12, 24)

        match from <= date && date <= until with
        | false -> None
        | true -> Some(Taisho(date.Year - 11 - 1900))

    let name (era: JapaneseEra) =
        match era with
        | Reiwa _ -> "令和"
        | Heisei _ -> "平成"
        | Showa _ -> "昭和"
        | Taisho _ -> "大正"
        | Meiji _ -> "明治"

    let fromDateOnly (date: DateOnly) =
        reiwa date
        |> Option.orElse (heisei date)
        |> Option.orElse (showa date)
        |> Option.orElse (taisho date)
        |> Option.orElse (meiji date)
