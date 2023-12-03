module Koyomi.FSharp.Tests.Year2024Tests

open System

open Xunit
open FsUnit.Xunit

open Koyomi.FSharp

module JanuaryTests =
    let January: obj[] list =
        [
            [| DateTime(2024, 1, 1); Some "元日"|]
            [| DateTime(2024, 1, 2); None |]
            [| DateTime(2024, 1, 3); None |]
            [| DateTime(2024, 1, 4); None |]
            [| DateTime(2024, 1, 5); None |]
            [| DateTime(2024, 1, 6); None |]
            [| DateTime(2024, 1, 7); None |]
            [| DateTime(2024, 1, 8); Some "成人の日" |]
            [| DateTime(2024, 1, 9); None |]
            [| DateTime(2024, 1, 10); None |]
            [| DateTime(2024, 1, 11); None |]
            [| DateTime(2024, 1, 12); None |]
            [| DateTime(2024, 1, 13); None |]
            [| DateTime(2024, 1, 14); None |]
            [| DateTime(2024, 1, 15); None |]
            [| DateTime(2024, 1, 16); None |]
            [| DateTime(2024, 1, 17); None |]
            [| DateTime(2024, 1, 18); None |]
            [| DateTime(2024, 1, 19); None |]
            [| DateTime(2024, 1, 20); None |]
            [| DateTime(2024, 1, 21); None |]
            [| DateTime(2024, 1, 22); None |]
            [| DateTime(2024, 1, 23); None |]
            [| DateTime(2024, 1, 24); None |]
            [| DateTime(2024, 1, 25); None |]
            [| DateTime(2024, 1, 26); None |]
            [| DateTime(2024, 1, 27); None |]
            [| DateTime(2024, 1, 28); None |]
            [| DateTime(2024, 1, 29); None |]
            [| DateTime(2024, 1, 30); None |]
            [| DateTime(2024, 1, 31); None |]
        ]

    [<Theory>]
    [<MemberData("January")>]
    let ``2024年1月の暦を判定できる`` (dt: DateTime) (expect: string option) =
        Koyomi.from dt
        |> Koyomi.holiday
        |> should equal expect

module FebruaryTests =
    let February: obj[] list =
        [
            [| DateTime(2024, 2, 1); None |]
            [| DateTime(2024, 2, 2); None |]
            [| DateTime(2024, 2, 3); None |]
            [| DateTime(2024, 2, 4); None |]
            [| DateTime(2024, 2, 5); None |]
            [| DateTime(2002, 2, 6); None |]
            [| DateTime(2024, 2, 7); None |]
            [| DateTime(2024, 2, 8); None |]
            [| DateTime(2024, 2, 9); None |]
            [| DateTime(2024, 2, 10); None |]
            [| DateTime(2024, 2, 11); Some "建国記念の日" |]
            [| DateTime(2024, 2, 12); Some "振替休日" |]
            [| DateTime(2024, 2, 13); None |]
            [| DateTime(2024, 2, 14); None |]
            [| DateTime(2024, 2, 15); None |]
            [| DateTime(2024, 2, 16); None |]
            [| DateTime(2024, 2, 17); None |]
            [| DateTime(2024, 2, 18); None |]
            [| DateTime(2024, 2, 19); None |]
            [| DateTime(2024, 2, 20); None |]
            [| DateTime(2024, 2, 21); None |]
            [| DateTime(2024, 2, 22); None |]
            [| DateTime(2024, 2, 23); Some "天皇誕生日" |]
            [| DateTime(2024, 2, 24); None |]
            [| DateTime(2024, 2, 25); None |]
            [| DateTime(2024, 2, 26); None |]
            [| DateTime(2024, 2, 27); None |]
            [| DateTime(2024, 2, 28); None |]
            [| DateTime(2024, 2, 29); None |]
        ]

    [<Theory>]
    [<MemberData("February")>]
    let ``2024年2月の暦を判定できる`` (dt: DateTime) (expect: string option) =
        Koyomi.from dt
        |> Koyomi.holiday
        |> should equal expect

module MarchTests =
    let March: obj[] list =
        [
            [| DateTime(2024, 3, 1); None |]
            [| DateTime(2024, 3, 2); None |]
            [| DateTime(2024, 3, 3); None |]
            [| DateTime(2024, 3, 4); None |]
            [| DateTime(2024, 3, 5); None |]
            [| DateTime(2024, 3, 6); None |]
            [| DateTime(2024, 3, 7); None |]
            [| DateTime(2024, 3, 8); None |]
            [| DateTime(2024, 3, 9); None |]
            [| DateTime(2024, 3, 10); None |]
            [| DateTime(2024, 3, 11); None |]
            [| DateTime(2024, 3, 12); None |]
            [| DateTime(2024, 3, 13); None |]
            [| DateTime(2024, 3, 14); None |]
            [| DateTime(2024, 3, 15); None |]
            [| DateTime(2024, 3, 16); None |]
            [| DateTime(2024, 3, 17); None |]
            [| DateTime(2024, 3, 18); None |]
            [| DateTime(2024, 3, 19); None |]
            [| DateTime(2024, 3, 20); Some "春分の日" |]
            [| DateTime(2024, 3, 21); None |]
            [| DateTime(2024, 3, 22); None |]
            [| DateTime(2024, 3, 23); None |]
            [| DateTime(2024, 3, 24); None |]
            [| DateTime(2024, 3, 25); None |]
            [| DateTime(2024, 3, 26); None |]
            [| DateTime(2024, 3, 27); None |]
            [| DateTime(2024, 3, 28); None |]
            [| DateTime(2024, 3, 29); None |]
            [| DateTime(2024, 3, 30); None |]
            [| DateTime(2024, 3, 31); None |]
        ]

    [<Theory>]
    [<MemberData("March")>]
    let ``2024年3月の暦を判定できる`` (dt: DateTime) (expect: string option) =
        Koyomi.from dt
        |> Koyomi.holiday
        |> should equal expect

module AprilTests =
    let April: obj[] list =
        [
            [| DateTime(2024, 4, 1); None |]
            [| DateTime(2024, 4, 2); None |]
            [| DateTime(2024, 4, 3); None |]
            [| DateTime(2024, 4, 4); None |]
            [| DateTime(2024, 4, 5); None |]
            [| DateTime(2024, 4, 6); None |]
            [| DateTime(2024, 4, 7); None |]
            [| DateTime(2024, 4, 8); None |]
            [| DateTime(2024, 4, 9); None |]
            [| DateTime(2024, 4, 10); None |]
            [| DateTime(2024, 4, 11); None |]
            [| DateTime(2024, 4, 12); None |]
            [| DateTime(2024, 4, 13); None |]
            [| DateTime(2024, 4, 14); None |]
            [| DateTime(2024, 4, 15); None |]
            [| DateTime(2024, 4, 16); None |]
            [| DateTime(2024, 4, 17); None |]
            [| DateTime(2024, 4, 18); None |]
            [| DateTime(2024, 4, 19); None |]
            [| DateTime(2024, 4, 20); None |]
            [| DateTime(2024, 4, 21); None |]
            [| DateTime(2024, 4, 22); None |]
            [| DateTime(2024, 4, 23); None |]
            [| DateTime(2024, 4, 24); None |]
            [| DateTime(2024, 4, 25); None |]
            [| DateTime(2024, 4, 26); None |]
            [| DateTime(2024, 4, 27); None |]
            [| DateTime(2024, 4, 28); None |]
            [| DateTime(2024, 4, 29); Some "昭和の日" |]
            [| DateTime(2024, 4, 30); None |]
        ]

    [<Theory>]
    [<MemberData("April")>]
    let ``2024年4月の暦を判定できる`` (dt: DateTime) (expect: string option) =
        Koyomi.from dt
        |> Koyomi.holiday
        |> should equal expect

module MayTests =
    let May: obj[] list =
        [
            [| DateTime(2024, 5, 1); None |]
            [| DateTime(2024, 5, 2); None |]
            [| DateTime(2024, 5, 3); Some "憲法記念日" |]
            [| DateTime(2024, 5, 4); Some "みどりの日" |]
            [| DateTime(2024, 5, 5); Some "こどもの日" |]
            [| DateTime(2024, 5, 6); Some "振替休日" |]
            [| DateTime(2024, 5, 7); None |]
            [| DateTime(2024, 5, 8); None |]
            [| DateTime(2024, 5, 9); None |]
            [| DateTime(2024, 5, 10); None |]
            [| DateTime(2024, 5, 11); None |]
            [| DateTime(2024, 5, 12); None |]
            [| DateTime(2024, 5, 13); None |]
            [| DateTime(2024, 5, 14); None |]
            [| DateTime(2024, 5, 15); None |]
            [| DateTime(2024, 5, 16); None |]
            [| DateTime(2024, 5, 17); None |]
            [| DateTime(2024, 5, 18); None |]
            [| DateTime(2024, 5, 19); None |]
            [| DateTime(2024, 5, 20); None |]
            [| DateTime(2024, 5, 21); None |]
            [| DateTime(2024, 5, 22); None |]
            [| DateTime(2024, 5, 23); None |]
            [| DateTime(2024, 5, 24); None |]
            [| DateTime(2024, 5, 25); None |]
            [| DateTime(2024, 5, 26); None |]
            [| DateTime(2024, 5, 27); None |]
            [| DateTime(2024, 5, 28); None |]
            [| DateTime(2024, 5, 29); None |]
            [| DateTime(2024, 5, 30); None |]
            [| DateTime(2024, 5, 31); None |]
        ]

    [<Theory>]
    [<MemberData("May")>]
    let ``2024年5月の暦を判定できる`` (dt: DateTime) (expect: string option) =
        Koyomi.from dt
        |> Koyomi.holiday
        |> should equal expect

module JuneTets =
    let June: obj[] list =
        [
            [| DateTime(2024, 6, 1); None |]
            [| DateTime(2024, 6, 2); None |]
            [| DateTime(2024, 6, 3); None |]
            [| DateTime(2024, 6, 4); None |]
            [| DateTime(2024, 6, 5); None |]
            [| DateTime(2024, 6, 6); None |]
            [| DateTime(2024, 6, 7); None |]
            [| DateTime(2024, 6, 8); None |]
            [| DateTime(2024, 6, 9); None |]
            [| DateTime(2024, 6, 10); None |]
            [| DateTime(2024, 6, 11); None |]
            [| DateTime(2024, 6, 12); None |]
            [| DateTime(2024, 6, 13); None |]
            [| DateTime(2024, 6, 14); None |]
            [| DateTime(2024, 6, 15); None |]
            [| DateTime(2024, 6, 16); None |]
            [| DateTime(2024, 6, 17); None |]
            [| DateTime(2024, 6, 18); None |]
            [| DateTime(2024, 6, 19); None |]
            [| DateTime(2024, 6, 20); None |]
            [| DateTime(2024, 6, 21); None |]
            [| DateTime(2024, 6, 22); None |]
            [| DateTime(2024, 6, 23); None |]
            [| DateTime(2024, 6, 24); None |]
            [| DateTime(2024, 6, 25); None |]
            [| DateTime(2024, 6, 26); None |]
            [| DateTime(2024, 6, 27); None |]
            [| DateTime(2024, 6, 28); None |]
            [| DateTime(2024, 6, 29); None |]
            [| DateTime(2024, 6, 30); None |]
        ]

    [<Theory>]
    [<MemberData("June")>]
    let ``2024年6月の暦を判定できる`` (dt: DateTime) (expect: string option) =
        Koyomi.from dt
        |> Koyomi.holiday
        |> should equal expect

module JulyTests =
    let July: obj[] list =
        [
            [| DateTime(2024, 7, 1); None |]
            [| DateTime(2024, 7, 2); None |]
            [| DateTime(2024, 7, 3); None |]
            [| DateTime(2024, 7, 4); None |]
            [| DateTime(2024, 7, 5); None |]
            [| DateTime(2024, 7, 6); None |]
            [| DateTime(2024, 7, 7); None |]
            [| DateTime(2024, 7, 8); None |]
            [| DateTime(2024, 7, 9); None |]
            [| DateTime(2024, 7, 10); None |]
            [| DateTime(2024, 7, 11); None |]
            [| DateTime(2024, 7, 12); None |]
            [| DateTime(2024, 7, 13); None |]
            [| DateTime(2024, 7, 14); None |]
            [| DateTime(2024, 7, 15); Some "海の日" |]
            [| DateTime(2024, 7, 16); None |]
            [| DateTime(2024, 7, 17); None |]
            [| DateTime(2024, 7, 18); None |]
            [| DateTime(2024, 7, 19); None |]
            [| DateTime(2024, 7, 20); None |]
            [| DateTime(2024, 7, 21); None |]
            [| DateTime(2024, 7, 22); None |]
            [| DateTime(2024, 7, 23); None |]
            [| DateTime(2024, 7, 24); None |]
            [| DateTime(2024, 7, 25); None |]
            [| DateTime(2024, 7, 26); None |]
            [| DateTime(2024, 7, 27); None |]
            [| DateTime(2024, 7, 28); None |]
            [| DateTime(2024, 7, 29); None |]
            [| DateTime(2024, 7, 30); None |]
            [| DateTime(2024, 7, 31); None |]
        ]

    [<Theory>]
    [<MemberData("July")>]
    let ``2024年7月の暦を判定できる`` (dt: DateTime) (expect: string option) =
        Koyomi.from dt
        |> Koyomi.holiday
        |> should equal expect

module AugustTests =
    let August: obj[] list =
        [
            [| DateTime(2024, 8, 1); None |]
            [| DateTime(2024, 8, 2); None |]
            [| DateTime(2024, 8, 3); None |]
            [| DateTime(2024, 8, 4); None |]
            [| DateTime(2024, 8, 5); None |]
            [| DateTime(2024, 8, 6); None |]
            [| DateTime(2024, 8, 7); None |]
            [| DateTime(2024, 8, 8); None |]
            [| DateTime(2024, 8, 9); None |]
            [| DateTime(2024, 8, 10); None |]
            [| DateTime(2024, 8, 11); Some "山の日" |]
            [| DateTime(2024, 8, 12); Some "振替休日" |]
            [| DateTime(2024, 8, 13); None |]
            [| DateTime(2024, 8, 14); None |]
            [| DateTime(2024, 8, 15); None |]
            [| DateTime(2024, 8, 16); None |]
            [| DateTime(2024, 8, 17); None |]
            [| DateTime(2024, 8, 18); None |]
            [| DateTime(2024, 8, 19); None |]
            [| DateTime(2024, 8, 20); None |]
            [| DateTime(2024, 8, 21); None |]
            [| DateTime(2024, 8, 22); None |]
            [| DateTime(2024, 8, 23); None |]
            [| DateTime(2024, 8, 24); None |]
            [| DateTime(2024, 8, 25); None |]
            [| DateTime(2024, 8, 26); None |]
            [| DateTime(2024, 8, 27); None |]
            [| DateTime(2024, 8, 28); None |]
            [| DateTime(2024, 8, 29); None |]
            [| DateTime(2024, 8, 30); None |]
            [| DateTime(2024, 8, 31); None |]
        ]

    [<Theory>]
    [<MemberData("August")>]
    let ``2024年8月の暦を判定できる`` (dt: DateTime) (expect: string option) =
        Koyomi.from dt
        |> Koyomi.holiday
        |> should equal expect

module SeptemberTests =
    let September: obj[] list =
        [
            [| DateTime(2024, 9, 1); None |]
            [| DateTime(2024, 9, 2); None |]
            [| DateTime(2024, 9, 3); None |]
            [| DateTime(2024, 9, 4); None |]
            [| DateTime(2024, 9, 5); None |]
            [| DateTime(2024, 9, 6); None |]
            [| DateTime(2024, 9, 7); None |]
            [| DateTime(2024, 9, 8); None |]
            [| DateTime(2024, 9, 9); None |]
            [| DateTime(2024, 9, 10); None |]
            [| DateTime(2024, 9, 11); None |]
            [| DateTime(2024, 9, 12); None |]
            [| DateTime(2024, 9, 13); None |]
            [| DateTime(2024, 9, 14); None |]
            [| DateTime(2024, 9, 15); None |]
            [| DateTime(2024, 9, 16); Some "敬老の日" |]
            [| DateTime(2024, 9, 17); None |]
            [| DateTime(2024, 9, 18); None |]
            [| DateTime(2024, 9, 19); None |]
            [| DateTime(2024, 9, 20); None |]
            [| DateTime(2024, 9, 21); None |]
            [| DateTime(2024, 9, 22); Some "秋分の日" |]
            [| DateTime(2024, 9, 23); Some "振替休日" |]
            [| DateTime(2024, 9, 24); None |]
            [| DateTime(2024, 9, 25); None |]
            [| DateTime(2024, 9, 26); None |]
            [| DateTime(2024, 9, 27); None |]
            [| DateTime(2024, 9, 28); None |]
            [| DateTime(2024, 9, 29); None |]
            [| DateTime(2024, 9, 30); None |]
        ]

    [<Theory>]
    [<MemberData("September")>]
    let ``2024年9月の暦を判定できる`` (dt: DateTime) (expect: string option) =
        Koyomi.from dt
        |> Koyomi.holiday
        |> should equal expect

module OctoberTests =
    let October: obj[] list =
        [
            [| DateTime(2024, 10, 1); None |]
            [| DateTime(2024, 10, 2); None |]
            [| DateTime(2024, 10, 3); None |]
            [| DateTime(2024, 10, 4); None |]
            [| DateTime(2024, 10, 5); None |]
            [| DateTime(2024, 10, 6); None |]
            [| DateTime(2024, 10, 7); None |]
            [| DateTime(2024, 10, 8); None |]
            [| DateTime(2024, 10, 9); None |]
            [| DateTime(2024, 10, 10); None |]
            [| DateTime(2024, 10, 11); None |]
            [| DateTime(2024, 10, 12); None |]
            [| DateTime(2024, 10, 13); None |]
            [| DateTime(2024, 10, 14); Some "スポーツの日" |]
            [| DateTime(2024, 10, 15); None |]
            [| DateTime(2024, 10, 16); None |]
            [| DateTime(2024, 10, 17); None |]
            [| DateTime(2024, 10, 18); None |]
            [| DateTime(2024, 10, 19); None |]
            [| DateTime(2024, 10, 20); None |]
            [| DateTime(2024, 10, 21); None |]
            [| DateTime(2024, 10, 22); None |]
            [| DateTime(2024, 10, 23); None |]
            [| DateTime(2024, 10, 24); None |]
            [| DateTime(2024, 10, 25); None |]
            [| DateTime(2024, 10, 26); None |]
            [| DateTime(2024, 10, 27); None |]
            [| DateTime(2024, 10, 28); None |]
            [| DateTime(2024, 10, 29); None |]
            [| DateTime(2024, 10, 30); None |]
            [| DateTime(2024, 10, 31); None |]
        ]

    [<Theory>]
    [<MemberData("October")>]
    let ``2024年10月の暦を判定できる`` (dt: DateTime) (expect: string option) =
        Koyomi.from dt
        |> Koyomi.holiday
        |> should equal expect

module NovemberTests =
    let November: obj[] list =
        [
            [| DateTime(2024, 11, 1); None |]
            [| DateTime(2024, 11, 2); None |]
            [| DateTime(2024, 11, 3); Some "文化の日" |]
            [| DateTime(2024, 11, 4); Some "振替休日" |]
            [| DateTime(2024, 11, 5); None |]
            [| DateTime(2024, 11, 6); None |]
            [| DateTime(2024, 11, 7); None |]
            [| DateTime(2024, 11, 8); None |]
            [| DateTime(2024, 11, 9); None |]
            [| DateTime(2024, 11, 10); None |]
            [| DateTime(2024, 11, 11); None |]
            [| DateTime(2024, 11, 12); None |]
            [| DateTime(2024, 11, 13); None |]
            [| DateTime(2024, 11, 14); None |]
            [| DateTime(2024, 11, 15); None |]
            [| DateTime(2024, 11, 16); None |]
            [| DateTime(2024, 11, 17); None |]
            [| DateTime(2024, 11, 18); None |]
            [| DateTime(2024, 11, 19); None |]
            [| DateTime(2024, 11, 20); None |]
            [| DateTime(2024, 11, 21); None |]
            [| DateTime(2024, 11, 22); None |]
            [| DateTime(2024, 11, 23); Some "勤労感謝の日" |]
            [| DateTime(2024, 11, 24); None |]
            [| DateTime(2024, 11, 25); None |]
            [| DateTime(2024, 11, 26); None |]
            [| DateTime(2024, 11, 27); None |]
            [| DateTime(2024, 11, 28); None |]
            [| DateTime(2024, 11, 29); None |]
            [| DateTime(2024, 11, 30); None |]
        ]

    [<Theory>]
    [<MemberData("November")>]
    let ``2024年11月の暦を判定できる`` (dt: DateTime) (expect: string option) =
        Koyomi.from dt
        |> Koyomi.holiday
        |> should equal expect

module DecemberTests =
    let December: obj[] list =
        [
            [| DateTime(2024, 12, 1); None |]
            [| DateTime(2024, 12, 2); None |]
            [| DateTime(2024, 12, 3); None |]
            [| DateTime(2024, 12, 4); None |]
            [| DateTime(2024, 12, 5); None |]
            [| DateTime(2024, 12, 6); None |]
            [| DateTime(2024, 12, 7); None |]
            [| DateTime(2024, 12, 8); None |]
            [| DateTime(2024, 12, 9); None |]
            [| DateTime(2024, 12, 10); None |]
            [| DateTime(2024, 12, 11); None |]
            [| DateTime(2024, 12, 12); None |]
            [| DateTime(2024, 12, 13); None |]
            [| DateTime(2024, 12, 14); None |]
            [| DateTime(2024, 12, 15); None |]
            [| DateTime(2024, 12, 16); None |]
            [| DateTime(2024, 12, 17); None |]
            [| DateTime(2024, 12, 18); None |]
            [| DateTime(2024, 12, 19); None |]
            [| DateTime(2024, 12, 20); None |]
            [| DateTime(2024, 12, 21); None |]
            [| DateTime(2024, 12, 22); None |]
            [| DateTime(2024, 12, 23); None |]
            [| DateTime(2024, 12, 24); None |]
            [| DateTime(2024, 12, 25); None |]
            [| DateTime(2024, 12, 26); None |]
            [| DateTime(2024, 12, 27); None |]
            [| DateTime(2024, 12, 28); None |]
            [| DateTime(2024, 12, 29); None |]
            [| DateTime(2024, 12, 30); None |]
            [| DateTime(2024, 12, 31); None |]
        ]

    [<Theory>]
    [<MemberData("December")>]
    let ``2024年12月の暦を判定できる`` (dt: DateTime) (expect: string option) =
        Koyomi.from dt
        |> Koyomi.holiday
        |> should equal expect
