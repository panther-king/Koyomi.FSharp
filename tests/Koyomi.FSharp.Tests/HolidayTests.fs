module Koyomi.FSharp.Tests.HolidayTests

open System

open Xunit
open FsUnit.Xunit

open Koyomi.FSharp

module NewYearsDayTests =
    let afterEstablished: obj[] seq =
        seq { 1949 .. DateTime.Now.Year }
        |> Seq.map (fun y -> [| y |])

    [<Theory>]
    [<MemberData("afterEstablished")>]
    let ``祝日法の施行後、1月1日は元日で祝日になる`` (y: int) =
        DateTime(y, 1, 1)
        |> newYearsDay
        |> isOkWith "元日"
        |> should be True

    [<Fact>]
    let ``祝日法の施行以前は1月1日であっても祝日ではない`` () =
        DateTime(1948, 1, 1)
        |> newYearsDay
        |> isOkWith "元日"
        |> should be False

module ComingOfAgeDayTests =
    let beforeHappyMonday: obj[] seq =
        seq { 1949 .. 1999}
        |> Seq.map (fun y -> [| y |])

    [<Theory>]
    [<MemberData("beforeHappyMonday")>]
    let ``祝日法の施行後、1月15日は成人の日で祝日になる`` (y: int) =
        DateTime(y, 1, 15)
        |> comingOfAgeDay
        |> isOkWith "成人の日"
        |> should be True

    [<Fact>]
    let ``祝日法の施行以前は1月15日であっても祝日ではない`` () =
        DateTime(1948, 1, 15)
        |> comingOfAgeDay
        |> isOkWith "成人の日"
        |> should be False

    let afterHappyMonday: obj[] list =
        [
            [| 2000; 1; 10 |]
            [| 2001; 1; 8 |]
            [| 2002; 1; 14 |]
            [| 2003; 1; 13 |]
            [| 2004; 1; 12 |]
            [| 2005; 1; 10 |]
            [| 2006; 1; 9 |]
            [| 2007; 1; 8 |]
            [| 2008; 1; 14 |]
            [| 2009; 1; 12 |]
            [| 2010; 1; 11 |]
            [| 2011; 1; 10 |]
            [| 2012; 1; 9 |]
            [| 2013; 1; 14 |]
            [| 2014; 1; 13 |]
            [| 2015; 1; 12 |]
            [| 2016; 1; 11 |]
            [| 2017; 1; 9 |]
            [| 2018; 1; 8 |]
            [| 2019; 1; 14 |]
            [| 2020; 1; 13 |]
            [| 2021; 1; 11 |]
            [| 2022; 1; 10 |]
        ]

    [<Theory>]
    [<MemberData("afterHappyMonday")>]
    let ``祝日法の改正後、1月の第2月曜が成人の日で祝日になる`` (y: int) (m: int) (d: int) =
        DateTime(y, m, d)
        |> comingOfAgeDay
        |> isOkWith "成人の日"
        |> should be True

module NationalFoundationDayTests =
    let afterEstablished: obj[] seq =
        seq { 1967 .. DateTime.Now.Year }
        |> Seq.map (fun y -> [| y |])

    [<Theory>]
    [<MemberData("afterEstablished")>]
    let ``1967年以降、2月15日は建国記念の日で祝日になる`` (y: int) =
        DateTime(y, 2, 11)
        |> nationalFoundationDay
        |> isOkWith "建国記念の日"
        |> should be True

    [<Fact>]
    let ``1967年より前は、2月15日であっても祝日ではない`` () =
        DateTime(1966, 2, 15)
        |> nationalFoundationDay
        |> isOkWith "建国記念の日"
        |> should be False

module EmperorsBirthdayTests =
    let showaEmperor: obj[] seq =
        seq { 1949 .. 1988 }
        |> Seq.map (fun y -> [| y |])

    [<Theory>]
    [<MemberData("showaEmperor")>]
    let ``昭和天皇在位時は、4月29日が天皇誕生日で祝日になる`` (y: int) =
        DateTime(y, 4, 29)
        |> emperorsBirthday
        |> isOkWith "天皇誕生日"
        |> should be True

    let heiseiEmperor: obj[] seq =
        seq { 1989 .. 2018 }
        |> Seq.map (fun y -> [| y |])

    [<Theory>]
    [<MemberData("heiseiEmperor")>]
    let ``平成天皇在位時は、12月23日が天皇誕生日で祝日になる`` (y: int) =
        DateTime(y, 12, 23)
        |> emperorsBirthday
        |> isOkWith "天皇誕生日"
        |> should be True

    let reiwaEmperor: obj[] seq =
        seq { 2020 .. DateTime.Now.Year }
        |> Seq.map (fun y -> [| y |])

    [<Theory>]
    [<MemberData("reiwaEmperor")>]
    let ``令和天皇在位時は、2月23日が天皇誕生日で祝日になる`` (y: int) =
        DateTime(y, 2, 23)
        |> emperorsBirthday
        |> isOkWith "天皇誕生日"
        |> should be True

    let emperorBirthdayDoesNotExists: obj[] list =
        [
            [| 12; 23 |]
            [| 2; 23 |]
        ]

    [<Theory>]
    [<MemberData("emperorBirthdayDoesNotExists")>]
    let ``2019年は天皇退位により天皇誕生日が存在しない`` (m: int) (d: int) =
        DateTime(2019, m, d)
        |> emperorsBirthday
        |> isOkWith "天皇誕生日"
        |> should be False

    [<Fact>]
    let ``祝日法制定以前は、昭和天皇の誕生日であっても祝日ではない`` () =
        DateTime(1948, 4, 29)
        |> emperorsBirthday
        |> isOkWith "天皇誕生日"
        |> should be False

module VernalEquinoxDayTests =
    let vernalEquinox: obj[] list =
        [
            [| 1949; 3; 21 |]
            [| 1950; 3; 21 |]
            [| 1951; 3; 21 |]
            [| 1952; 3; 21 |]
            [| 1953; 3; 21 |]
            [| 1954; 3; 21 |]
            [| 1955; 3; 21 |]
            [| 1956; 3; 21 |]
            [| 1957; 3; 21 |]
            [| 1958; 3; 21 |]
            [| 1959; 3; 21 |]
            [| 1960; 3; 20 |]
            [| 1961; 3; 21 |]
            [| 1962; 3; 21 |]
            [| 1963; 3; 21 |]
            [| 1964; 3; 20 |]
            [| 1965; 3; 21 |]
            [| 1966; 3; 21 |]
            [| 1967; 3; 21 |]
            [| 1968; 3; 20 |]
            [| 1969; 3; 21 |]
            [| 1970; 3; 21 |]
            [| 1971; 3; 21 |]
            [| 1972; 3; 20 |]
            [| 1973; 3; 21 |]
            [| 1974; 3; 21 |]
            [| 1975; 3; 21 |]
            [| 1976; 3; 20 |]
            [| 1977; 3; 21 |]
            [| 1978; 3; 21 |]
            [| 1979; 3; 21 |]
            [| 1980; 3; 20 |]
            [| 1981; 3; 21 |]
            [| 1982; 3; 21 |]
            [| 1983; 3; 21 |]
            [| 1984; 3; 20 |]
            [| 1985; 3; 21 |]
            [| 1986; 3; 21 |]
            [| 1987; 3; 21 |]
            [| 1988; 3; 20 |]
            [| 1989; 3; 21 |]
            [| 1990; 3; 21 |]
            [| 1991; 3; 21 |]
            [| 1992; 3; 20 |]
            [| 1993; 3; 20 |]
            [| 1994; 3; 21 |]
            [| 1995; 3; 21 |]
            [| 1996; 3; 20 |]
            [| 1997; 3; 20 |]
            [| 1998; 3; 21 |]
            [| 1999; 3; 21 |]
            [| 2000; 3; 20 |]
            [| 2001; 3; 20 |]
            [| 2002; 3; 21 |]
            [| 2003; 3; 21 |]
            [| 2004; 3; 20 |]
            [| 2005; 3; 20 |]
            [| 2006; 3; 21 |]
            [| 2007; 3; 21 |]
            [| 2008; 3; 20 |]
            [| 2009; 3; 20 |]
            [| 2010; 3; 21 |]
            [| 2011; 3; 21 |]
            [| 2012; 3; 20 |]
            [| 2013; 3; 20 |]
            [| 2014; 3; 21 |]
            [| 2015; 3; 21 |]
            [| 2016; 3; 20 |]
            [| 2017; 3; 20 |]
            [| 2018; 3; 21 |]
            [| 2019; 3; 21 |]
            [| 2020; 3; 20 |]
            [| 2021; 3; 20 |]
            [| 2022; 3; 21 |]
        ]

    [<Theory>]
    [<MemberData("vernalEquinox")>]
    let ``祝日法の施行後、春分日は春分の日で祝日になる`` (y: int) (m: int) (d: int) =
        DateTime(y, m, d)
        |> vernalEquinoxDay
        |> isOkWith "春分の日"
        |> should be True

    [<Fact>]
    let ``祝日法の施行以前は春分日であっても祝日ではない`` () =
        DateTime(1948, 3, 21)
        |> vernalEquinoxDay
        |> isOkWith "春分の日"
        |> should be False
