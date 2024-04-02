namespace Koyomi.FSharp

open System

type JapaneseMonth =
    | Mutsuki
    | Kisaragi
    | Yayoi
    | Uzuki
    | Satsuki
    | Minazuki
    | Fumizuki
    | Hazuki
    | Nagatsuki
    | Kannazuki
    | Shimotsuki
    | Shiwasu

[<RequireQualifiedAccess>]
module JapaneseMonth =
    let private months =
        [| "睦月"; "如月"; "弥生"; "卯月"; "皐月"; "水無月"; "文月"; "葉月"; "長月"; "神無月"; "霜月"; "師走" |]

    let fromNumber (number: int) =
        match number with
        | 1 -> Some(Mutsuki)
        | 2 -> Some(Kisaragi)
        | 3 -> Some(Yayoi)
        | 4 -> Some(Uzuki)
        | 5 -> Some(Satsuki)
        | 6 -> Some(Minazuki)
        | 7 -> Some(Fumizuki)
        | 8 -> Some(Hazuki)
        | 9 -> Some(Nagatsuki)
        | 10 -> Some(Kannazuki)
        | 11 -> Some(Shimotsuki)
        | 12 -> Some(Shiwasu)
        | _ -> None

    let fromName (name: string) =
        months
        |> Array.tryFindIndex (fun n -> n = name)
        |> Option.bind (fun i -> fromNumber (i + 1))

    let number (month: JapaneseMonth) =
        match month with
        | Mutsuki -> 1
        | Kisaragi -> 2
        | Yayoi -> 3
        | Uzuki -> 4
        | Satsuki -> 5
        | Minazuki -> 6
        | Fumizuki -> 7
        | Hazuki -> 8
        | Nagatsuki -> 9
        | Kannazuki -> 10
        | Shimotsuki -> 11
        | Shiwasu -> 12

    let name (month: JapaneseMonth) = months[number month - 1]

    let fromDateOnly (date: DateOnly) = fromNumber date.Month |> Option.get
