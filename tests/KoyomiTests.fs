module Koyomi.FSharp.Tests.KoyomiTests

open System

open Xunit
open FsUnit.Xunit

open Koyomi.FSharp

[<Fact>]
let ``祝日情報を取得できる`` () =
    DateTime(2022, 5, 5)
    |> Koyomi.from
    |> Koyomi.holiday
    |> isSome "こどもの日"
    |> should be True

[<Fact>]
let ``祝日であることを判定できる`` () =
    DateTime(2022, 5, 3)
    |> Koyomi.from
    |> Koyomi.isHoliday
    |> should be True

[<Fact>]
let ``祝日でないことを判定できる`` () =
    DateTime(2022, 5, 10)
    |> Koyomi.from
    |> Koyomi.isHoliday
    |> should be False

[<Fact>]
let ``指定した形式でフォーマットできる`` () =
    DateTime(2022, 12, 1)
    |> Koyomi.from
    |> Koyomi.format "yyyy/MM/dd"
    |> should equal "2022/12/01"

[<Fact>]
let ``西暦を取得できる`` () =
    DateTime(2022, 1, 1)
    |> Koyomi.from
    |> Koyomi.year
    |> should equal 2022

[<Fact>]
let ``月を取得できる`` () =
    DateTime(2022, 12, 1)
    |> Koyomi.from
    |> Koyomi.month
    |> should equal 12

[<Fact>]
let ``日を取得できる`` () =
    DateTime(2022, 1, 20)
    |> Koyomi.from
    |> Koyomi.day
    |> should equal 20

[<Fact>]
let ``曜日を取得できる`` () =
    DateTime(2022, 1, 10)
    |> Koyomi.from
    |> Koyomi.dayOfWeek
    |> should equal DayOfWeek.Monday

[<Fact>]
let ``任意の年を加算できる`` () =
    DateTime(2022, 1, 10)
    |> Koyomi.from
    |> Koyomi.addYears 1
    |> should equal (DateTime(2023, 1, 10) |> Koyomi.from)

[<Fact>]
let ``任意の月を加算できる`` () =
    DateTime(2022, 1, 10)
    |> Koyomi.from
    |> Koyomi.addMonths 3
    |> should equal (DateTime(2022, 4, 10) |> Koyomi.from)

[<Fact>]
let ``任意の日を加算できる`` () =
    DateTime(2022, 1, 10)
    |> Koyomi.from
    |> Koyomi.addDays 50
    |> should equal (DateTime(2022, 3, 1) |> Koyomi.from)
