namespace Koyomi.FSharp.Tests

open System

open Xunit
open FsUnit.Xunit

open Koyomi.FSharp

module MonthTests =
    [<Fact>]
    let ``年月日から変換できる`` () =
        DateOnly(2024, 1, 1) |> JapaneseMonth.fromDateOnly |> should equal Mutsuki

    let namesOfMonths: obj[] list =
        [ [| "睦月"; Mutsuki |]
          [| "如月"; Kisaragi |]
          [| "弥生"; Yayoi |]
          [| "卯月"; Uzuki |]
          [| "皐月"; Satsuki |]
          [| "水無月"; Minazuki |]
          [| "文月"; Fumizuki |]
          [| "葉月"; Hazuki |]
          [| "長月"; Nagatsuki |]
          [| "神無月"; Kannazuki |]
          [| "霜月"; Shimotsuki |]
          [| "師走"; Shiwasu |] ]

    [<Theory>]
    [<MemberData("namesOfMonths")>]
    let ``月の名前から変換できる`` (name: string) (expect: JapaneseMonth) =
        JapaneseMonth.fromName name |> isSomeOf expect |> should be True

    [<Theory>]
    [<MemberData("namesOfMonths")>]
    let ``月名に変換できる`` (expect: string) (month: JapaneseMonth) =
        JapaneseMonth.name month |> should equal expect

    [<Fact>]
    let ``月の名前でなければ変換できない`` () =
        JapaneseMonth.fromName String.Empty |> should equal None

    let numbersOfMonths: obj[] list =
        [ [| 1; Mutsuki |]
          [| 2; Kisaragi |]
          [| 3; Yayoi |]
          [| 4; Uzuki |]
          [| 5; Satsuki |]
          [| 6; Minazuki |]
          [| 7; Fumizuki |]
          [| 8; Hazuki |]
          [| 9; Nagatsuki |]
          [| 10; Kannazuki |]
          [| 11; Shimotsuki |]
          [| 12; Shiwasu |] ]

    [<Theory>]
    [<MemberData("numbersOfMonths")>]
    let ``月番号から変換できる`` (number: int) (expect: JapaneseMonth) =
        JapaneseMonth.fromNumber number |> isSomeOf expect |> should be True

    [<Theory>]
    [<MemberData("numbersOfMonths")>]
    let ``月番号に変換できる`` (expect: int) (month: JapaneseMonth) =
        JapaneseMonth.number month |> should equal expect

    [<Fact>]
    let ``月番号でなければ変換できない`` () =
        JapaneseMonth.fromNumber 0 |> should equal None
