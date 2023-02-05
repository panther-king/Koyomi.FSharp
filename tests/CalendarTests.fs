module Koyomi.FSharp.Tests.CalendarTests

open System

open Xunit
open FsUnit.Xunit

open Koyomi.FSharp

[<Fact>]
let ``指定した期間のカレンダーを生成できる`` () =
    let from = DateTime(2022, 1, 1)
    let until = DateTime(2022, 12, 31)
    let cal = Calendar.between from until
    Seq.head cal |> should equal (Koyomi.from from)
    Seq.rev cal |> Seq.head |> should equal (Koyomi.from until)
    Seq.length cal |> should equal 365

[<Fact>]
let ``矛盾した期間のカレンダーは生成できない`` () =
    let from = DateTime(2022, 12, 31)
    let until = DateTime(2022, 1, 1)
    Calendar.between from until
    |> should be Empty

[<Fact>]
let ``指定した年月日から当日までのカレンダーを生成できる`` () =
    let from = DateTime.Now.AddDays -10
    let cal = Calendar.from from
    Seq.head cal |> should equal (Koyomi.from from)
    Seq.rev cal |> Seq.head |> should equal (Koyomi.from DateTime.Now)
    Seq.length cal |> should equal 11

[<Fact>]
let ``当日から指定した年月日までのカレンダーを生成できる`` () =
    let until = DateTime.Now.AddDays 10
    let cal = Calendar.until until
    Seq.head cal |> should equal (Koyomi.from DateTime.Now)
    Seq.rev cal |> Seq.head |> should equal (Koyomi.from until)
    Seq.length cal |> should equal 11

[<Fact>]
let ``指定した年月のカレンダーを生成できる`` () =
    let from = DateTime(2024, 2, 1)
    let until = DateTime(2024, 2, 29)
    let cal = Calendar.ofMonth 2024 2
    Seq.head cal |> should equal (Koyomi.from from)
    Seq.rev cal |> Seq.head |> should equal (Koyomi.from until)
    Seq.length cal |> should equal 29

[<Fact>]
let ``存在しない月のカレンダーは生成できない`` () =
    Calendar.ofMonth 2022 13
    |> should be Empty

[<Fact>]
let ``指定した年のカレンダーを生成できる`` () =
    let from = DateTime(2024, 1, 1)
    let until = DateTime(2024, 12, 31)
    let cal = Calendar.ofYear 2024
    Seq.head cal |> should equal (Koyomi.from from)
    Seq.rev cal |> Seq.head |> should equal (Koyomi.from until)
    Seq.length cal |> should equal 366

[<Fact>]
let ``カレンダーから祝日だけを抽出できる`` () =
    let cal = Calendar.ofYear 2023
    Calendar.ofHolidays cal
    |> Seq.length
    |> should equal 17
