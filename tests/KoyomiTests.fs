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
    Koyomi.init 2022 5 3
    |> Koyomi.isHoliday
    |> should be True

[<Fact>]
let ``祝日でないことを判定できる`` () =
    Koyomi.init 2022 5 10
    |> Koyomi.isHoliday
    |> should be False

[<Fact>]
let ``和暦情報を取得できる`` () =
    Koyomi.init 2022 1 1
    |> Koyomi.era
    |> should equal { Epoc = Reiwa
                      FullName = "令和4年"
                      Name = "令和"
                      Year = 4 }

[<Fact>]
let ``西暦を取得できる`` () =
    Koyomi.init 2022 1 1
    |> Koyomi.year
    |> should equal 2022

[<Fact>]
let ``月を取得できる`` () =
    Koyomi.init 2022 12 1
    |> Koyomi.month
    |> should equal 12

[<Fact>]
let ``日を取得できる`` () =
    Koyomi.init 2022 1 20
    |> Koyomi.day
    |> should equal 20

[<Fact>]
let ``曜日を取得できる`` () =
    Koyomi.init 2022 1 10
    |> Koyomi.dayOfWeek
    |> should equal DayOfWeek.Monday
