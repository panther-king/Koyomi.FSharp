module Koyomi.FSharp.Tests.HolidayTests

open System

open Xunit
open FsUnit.Xunit

open Koyomi.FSharp.Holiday

module NewYearsDayTests =
    let afterEstablished: obj[] seq =
        seq { 1949 .. DateTime.Now.Year }
        |> Seq.map (fun y -> [| y |])

    [<Theory>]
    [<MemberData("afterEstablished")>]
    let ``祝日法の施行後、1月1日は元日で祝日になる`` (y: int) =
        DateTime(y, 1, 1)
        |> NewYearsDay.orNot
        |> isOkWith "元日"
        |> should be True

    [<Fact>]
    let ``祝日法の施行以前は、1月1日であっても元日の祝日ではない`` () =
        DateTime(1948, 1, 1)
        |> NewYearsDay.orNot
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
        |> ComingOfAgeDay.orNot
        |> isOkWith "成人の日"
        |> should be True

    [<Fact>]
    let ``祝日法の施行以前は、1月15日であっても成人の日の祝日ではない`` () =
        DateTime(1948, 1, 15)
        |> ComingOfAgeDay.orNot
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
        |> ComingOfAgeDay.orNot
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
        |> NationalFoundationDay.orNot
        |> isOkWith "建国記念の日"
        |> should be True

    [<Fact>]
    let ``1967年より前は、2月15日であっても建国記念の日の祝日ではない`` () =
        DateTime(1966, 2, 15)
        |> NationalFoundationDay.orNot
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
        |> EmperorsBirthday.orNot
        |> isOkWith "天皇誕生日"
        |> should be True

    let heiseiEmperor: obj[] seq =
        seq { 1989 .. 2018 }
        |> Seq.map (fun y -> [| y |])

    [<Theory>]
    [<MemberData("heiseiEmperor")>]
    let ``平成天皇在位時は、12月23日が天皇誕生日で祝日になる`` (y: int) =
        DateTime(y, 12, 23)
        |> EmperorsBirthday.orNot
        |> isOkWith "天皇誕生日"
        |> should be True

    let reiwaEmperor: obj[] seq =
        seq { 2020 .. DateTime.Now.Year }
        |> Seq.map (fun y -> [| y |])

    [<Theory>]
    [<MemberData("reiwaEmperor")>]
    let ``令和天皇在位時は、2月23日が天皇誕生日で祝日になる`` (y: int) =
        DateTime(y, 2, 23)
        |> EmperorsBirthday.orNot
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
        |> EmperorsBirthday.orNot
        |> isOkWith "天皇誕生日"
        |> should be False

    [<Fact>]
    let ``祝日法制定以前は、昭和天皇の誕生日であっても天皇誕生日の祝日ではない`` () =
        DateTime(1948, 4, 29)
        |> EmperorsBirthday.orNot
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
        |> VernalEquinoxDay.orNot
        |> isOkWith "春分の日"
        |> should be True

    [<Fact>]
    let ``祝日法の施行以前は、春分日であっても春分の日の祝日ではない`` () =
        DateTime(1948, 3, 21)
        |> VernalEquinoxDay.orNot
        |> isOkWith "春分の日"
        |> should be False

module GreenDayTests =
    let between: obj[] seq =
        seq { 1989 .. 2006}
        |> Seq.map (fun y -> [| y |])

    [<Theory>]
    [<MemberData("between")>]
    let ``1989年から2006年は、4月29日がみどりの日で祝日になる`` (y: int) =
        DateTime(y, 4, 29)
        |> GreenDay.orNot
        |> isOkWith "みどりの日"
        |> should be True

    let after: obj[] seq =
        seq { 2007 .. DateTime.Now.Year}
        |> Seq.map (fun y -> [| y |])

    [<Theory>]
    [<MemberData("after")>]
    let ``2007年以降は、5月4日がみどりの日で祝日になる`` (y: int) =
        DateTime(y, 5, 4)
        |> GreenDay.orNot
        |> isOkWith "みどりの日"
        |> should be True

    [<Fact>]
    let ``1988年以前の4月29日はみどりの日ではない`` () =
        DateTime(1988, 4, 29)
        |> GreenDay.orNot
        |> isOkWith "みどりの日"
        |> should be False

module ShowaDayTests =
    let afterEstablished: obj[] seq =
        seq { 2007 .. DateTime.Now.Year }
        |> Seq.map (fun y -> [| y |])

    [<Theory>]
    [<MemberData("afterEstablished")>]
    let ``2007年以降、4月29日は昭和の日で祝日になる`` (y: int) =
        DateTime(2007, 4, 29)
        |> ShowaDay.orNot
        |> isOkWith "昭和の日"
        |> should be True

    [<Fact>]
    let ``2006年以前の4月29日は昭和の日ではない`` () =
        DateTime(2006, 4, 29)
        |> ShowaDay.orNot
        |> isOkWith "昭和の日"
        |> should be False

module ConstitutionDayTests =
    let afterEstablished: obj[] seq =
        seq { 1948 .. DateTime.Now.Year }
        |> Seq.map (fun y -> [| y |])

    [<Theory>]
    [<MemberData("afterEstablished")>]
    let ``祝日法の施行後、5月3日は憲法記念日で祝日になる`` (y: int) =
        DateTime(y, 5, 3)
        |> ConstitutionDay.orNot
        |> isOkWith "憲法記念日"
        |> should be True

    [<Fact>]
    let ``祝日法の施行以前は、5月3日であっても憲法記念日の祝日ではない`` () =
        DateTime(1947, 5, 3)
        |> ConstitutionDay.orNot
        |> isOkWith "憲法記念日"
        |> should be False

module ChildrensDayTests =
    let afterEstablished: obj[] seq =
        seq { 1948 .. DateTime.Now.Year }
        |> Seq.map (fun y -> [| y |])

    [<Theory>]
    [<MemberData("afterEstablished")>]
    let ``祝日法の施行後、5月5日はこどもの日で祝日になる`` (y: int) =
        DateTime(y, 5, 5)
        |> ChildrensDay.orNot
        |> isOkWith "こどもの日"
        |> should be True

    [<Fact>]
    let ``祝日法の施行以前は、5月5日であってもこどもの日の祝日ではない`` () =
        DateTime(1947, 5, 5)
        |> ChildrensDay.orNot
        |> isOkWith "こどもの日"
        |> should be False

module MarineDayTests =
    let betweenEstablished: obj[] seq =
        seq { 1996 .. 2002 }
        |> Seq.map (fun y -> [| y |])

    [<Theory>]
    [<MemberData("betweenEstablished")>]
    let ``1996年から2002年は、7月20日が海の日で祝日になる`` (y: int) =
        DateTime(y, 7, 20)
        |> MarineDay.orNot
        |> isOkWith "海の日"
        |> should be True

    let betweenAmended: obj[] list =
        [
            [| 2003; 7; 21 |]
            [| 2004; 7; 19 |]
            [| 2005; 7; 18 |]
            [| 2006; 7; 17 |]
            [| 2007; 7; 16 |]
            [| 2008; 7; 21 |]
            [| 2009; 7; 20 |]
            [| 2010; 7; 19 |]
            [| 2011; 7; 18 |]
            [| 2012; 7; 16 |]
            [| 2013; 7; 15 |]
            [| 2014; 7; 21 |]
            [| 2015; 7; 20 |]
            [| 2016; 7; 18 |]
            [| 2017; 7; 17 |]
            [| 2018; 7; 16 |]
            [| 2019; 7; 15 |]
        ]

    [<Theory>]
    [<MemberData("betweenAmended")>]
    let ``2003年から2019年は、7月の第3月曜が海の日で祝日になる`` (y: int) (m: int) (d: int) =
        DateTime(y, m, d)
        |> MarineDay.orNot
        |> isOkWith "海の日"
        |> should be True

    [<Fact>]
    let ``2020年は、東京五輪・パラリンピック特措法により7月23日が海の日で祝日になる`` () =
        DateTime(2020, 7, 23)
        |> MarineDay.orNot
        |> isOkWith "海の日"
        |> should be True

    [<Fact>]
    let ``2021年は東京オリンピック開会式前日の7月22日が海の日で祝日になる`` () =
        DateTime(2021, 7, 22)
        |> MarineDay.orNot
        |> isOkWith "海の日"
        |> should be True

    let afterOlympic: obj[] list =
        [
            [| 2022; 7; 18 |]
        ]

    [<Theory>]
    [<MemberData("afterOlympic")>]
    let ``東京オリンピック以降は、7月の第3月曜が海の日で祝日になる`` (y: int) (m: int) (d: int) =
        DateTime(y, m, d)
        |> MarineDay.orNot
        |> isOkWith "海の日"
        |> should be True

    [<Fact>]
    let ``海の日制定以前は、7月20日であっても海の日の祝日ではない`` () =
        DateTime(1995, 7, 20)
        |> MarineDay.orNot
        |> isOkWith "海の日"
        |> should be False

module MountainDayTests =
    let betweenEstablished: obj[] seq =
        seq { 2016 .. 2019 }
        |> Seq.map (fun y -> [| y |])

    [<Theory>]
    [<MemberData("betweenEstablished")>]
    let ``2016年から2019年は、8月11日が山の日で祝日になる`` (y: int) =
        DateTime(y, 8, 11)
        |> MountainDay.orNot
        |> isOkWith "山の日"
        |> should be True

    [<Fact>]
    let ``2020年は、東京五輪・パラリンピック特措法により8月10日が山の日で祝日になる`` () =
        DateTime(2020, 8, 10)
        |> MountainDay.orNot
        |> isOkWith "山の日"
        |> should be True

    [<Fact>]
    let ``2021年は、東京オリンピック閉会式当日の8月8日が山の日で祝日になる`` () =
        DateTime(2021, 8, 8)
        |> MountainDay.orNot
        |> isOkWith "山の日"
        |> should be True

    let afterOlympic: obj[] seq =
        seq { 2022 .. DateTime.Now.Year }
        |> Seq.map (fun y -> [| y |])

    [<Theory>]
    [<MemberData("afterOlympic")>]
    let ``東京オリンピック以降は、8月11日が山の日で祝日になる`` (y: int) =
        DateTime(y, 8, 11)
        |> MountainDay.orNot
        |> isOkWith "山の日"
        |> should be True

    [<Fact>]
    let ``山の日制定以前は、8月11日であっても山の日の祝日ではない`` () =
        DateTime(2015, 8, 11)
        |> MountainDay.orNot
        |> isOkWith "山の日"
        |> should be False

module RespectForTheAgedDayTests =
    let betweenEstablished: obj[] seq =
        seq { 1966 .. 2002 }
        |> Seq.map (fun y -> [| y |])

    [<Theory>]
    [<MemberData("betweenEstablished")>]
    let ``1966年から2002年は、9月15日が敬老の日で祝日になる`` (y: int) =
        DateTime(y, 9, 15)
        |> RespectForTheAgedDay.orNot
        |> isOkWith "敬老の日"
        |> should be True

    let afterHappyMonday: obj[] list =
        [
            [| 2003; 9; 15 |]
            [| 2004; 9; 20 |]
            [| 2005; 9; 19 |]
            [| 2006; 9; 18 |]
            [| 2007; 9; 17 |]
            [| 2008; 9; 15 |]
            [| 2009; 9; 21 |]
            [| 2010; 9; 20 |]
            [| 2011; 9; 19 |]
            [| 2012; 9; 17 |]
            [| 2013; 9; 16 |]
            [| 2014; 9; 15 |]
            [| 2015; 9; 21 |]
            [| 2016; 9; 19 |]
            [| 2017; 9; 18 |]
            [| 2018; 9; 17 |]
            [| 2019; 9; 16 |]
            [| 2020; 9; 21 |]
            [| 2021; 9; 20 |]
            [| 2022; 9; 19 |]
        ]

    [<Theory>]
    [<MemberData("afterHappyMonday")>]
    let ``2003年以降は、9月の第3月曜が敬老の日で祝日になる`` (y: int) (m: int) (d: int) =
        DateTime(y, m, d)
        |> RespectForTheAgedDay.orNot
        |> isOkWith "敬老の日"
        |> should be True

    [<Fact>]
    let ``敬老の日制定以前は9月15日であっても敬老の日の祝日ではない`` () =
        DateTime(1965, 9, 15)
        |> RespectForTheAgedDay.orNot
        |> isOkWith "敬老の日"
        |> should be False

module AutumnalEquinoxDayTests =
    let autumnalEquinox: obj[] list =
        [
            [| 1949; 9; 23 |]
            [| 1950; 9; 23 |]
            [| 1951; 9; 24 |]
            [| 1952; 9; 23 |]
            [| 1953; 9; 23 |]
            [| 1954; 9; 23 |]
            [| 1955; 9; 24 |]
            [| 1956; 9; 23 |]
            [| 1957; 9; 23 |]
            [| 1958; 9; 23 |]
            [| 1959; 9; 24 |]
            [| 1960; 9; 23 |]
            [| 1961; 9; 23 |]
            [| 1962; 9; 23 |]
            [| 1963; 9; 24 |]
            [| 1964; 9; 23 |]
            [| 1965; 9; 23 |]
            [| 1966; 9; 23 |]
            [| 1967; 9; 24 |]
            [| 1968; 9; 23 |]
            [| 1969; 9; 23 |]
            [| 1970; 9; 23 |]
            [| 1979; 9; 24 |]
            [| 1980; 9; 23 |]
            [| 1981; 9; 23 |]
            [| 1982; 9; 23 |]
            [| 1983; 9; 23 |]
            [| 1984; 9; 23 |]
            [| 1985; 9; 23 |]
            [| 1986; 9; 23 |]
            [| 1987; 9; 23 |]
            [| 1988; 9; 23 |]
            [| 1989; 9; 23 |]
            [| 1990; 9; 23 |]
            [| 1991; 9; 23 |]
            [| 1992; 9; 23 |]
            [| 1993; 9; 23 |]
            [| 1994; 9; 23 |]
            [| 1995; 9; 23 |]
            [| 1996; 9; 23 |]
            [| 1997; 9; 23 |]
            [| 1998; 9; 23 |]
            [| 1999; 9; 23 |]
            [| 2000; 9; 23 |]
            [| 2001; 9; 23 |]
            [| 2002; 9; 23 |]
            [| 2003; 9; 23 |]
            [| 2004; 9; 23 |]
            [| 2005; 9; 23 |]
            [| 2006; 9; 23 |]
            [| 2007; 9; 23 |]
            [| 2008; 9; 23 |]
            [| 2009; 9; 23 |]
            [| 2010; 9; 23 |]
            [| 2011; 9; 23 |]
            [| 2012; 9; 22 |]
            [| 2013; 9; 23 |]
            [| 2014; 9; 23 |]
            [| 2015; 9; 23 |]
            [| 2016; 9; 22 |]
            [| 2017; 9; 23 |]
            [| 2018; 9; 23 |]
            [| 2019; 9; 23 |]
            [| 2020; 9; 22 |]
            [| 2021; 9; 23 |]
            [| 2022; 9; 23 |]
        ]

    [<Theory>]
    [<MemberData("autumnalEquinox")>]
    let ``祝日法の施行後、秋分日は秋分の日で祝日になる`` (y: int) (m: int) (d: int) =
        DateTime(y, m, d)
        |> AutumnalEquinoxDay.orNot
        |> isOkWith "秋分の日"
        |> should be True

module PhysicalEducationDayTests =
    let betweenEstablished: obj[] seq =
        seq { 1966 .. 1999 }
        |> Seq.map (fun y -> [| y |])

    [<Theory>]
    [<MemberData("betweenEstablished")>]
    let ``1966年から1999年は、10月10日が体育の日で祝日になる`` (y: int) =
        DateTime(y, 10, 10)
        |> PhysicalEducationDay.orNot
        |> isOkWith "体育の日"
        |> should be True

    let afterHappyMonday: obj[] list =
        [
            [| 2000; 10; 9 |]
            [| 2001; 10; 8 |]
            [| 2002; 10; 14 |]
            [| 2003; 10; 13 |]
            [| 2004; 10; 11 |]
            [| 2005; 10; 10 |]
            [| 2006; 10; 9 |]
            [| 2007; 10; 8 |]
            [| 2008; 10; 13 |]
            [| 2009; 10; 12 |]
            [| 2010; 10; 11 |]
            [| 2011; 10; 10 |]
            [| 2012; 10; 8 |]
            [| 2013; 10; 14 |]
            [| 2014; 10; 13 |]
            [| 2015; 10; 12 |]
            [| 2016; 10; 10 |]
            [| 2017; 10; 9 |]
            [| 2018; 10; 8 |]
            [| 2019; 10; 14 |]
        ]

    [<Theory>]
    [<MemberData("afterHappyMonday")>]
    let ``2000年から2019年は、10月の第2月曜が体育の日で祝日になる`` (y: int) (m: int) (d: int) =
        DateTime(y, m, d)
        |> PhysicalEducationDay.orNot
        |> isOkWith "体育の日"
        |> should be True

    [<Fact>]
    let ``体育の日制定以前は10月10日であっても体育の日の祝日ではない`` () =
        DateTime(1965, 10, 10)
        |> PhysicalEducationDay.orNot
        |> isOkWith "体育の日"
        |> should be False

module SportsDayTests =
    [<Fact>]
    let ``2020年は、東京五輪・パラリンピック特措法により7月24日がスポーツの日で祝日になる`` () =
        DateTime(2020, 7, 24)
        |> SportsDay.orNot
        |> isOkWith "スポーツの日"
        |> should be True

    [<Fact>]
    let ``2021年は、東京オリンピック開会式当日の7月23日がスポーツの日で祝日になる`` () =
        DateTime(2021, 7, 23)
        |> SportsDay.orNot
        |> isOkWith "スポーツの日"
        |> should be True

    let afterOlympic: obj[] list =
        [
            [| 2022; 10; 10 |]
        ]

    [<Theory>]
    [<MemberData("afterOlympic")>]
    let ``東京オリンピック以降は、10月の第2月曜がスポーツの日で祝日になる`` (y: int) (m: int) (d: int) =
        DateTime(y, m, d)
        |> SportsDay.orNot
        |> isOkWith "スポーツの日"
        |> should be True

    [<Fact>]
    let ``スポーツの日制定以前は、10月の第2月曜であってもスポーツの日の祝日ではない`` () =
        DateTime(2019, 10, 14)
        |> SportsDay.orNot
        |> isOkWith "スポーツの日"
        |> should be False

module CultureDayTests =
    let afterEstablished: obj[] seq =
        seq { 1948 .. DateTime.Now.Year }
        |> Seq.map (fun y -> [| y |])

    [<Theory>]
    [<MemberData("afterEstablished")>]
    let ``祝日法の施行後、11月3日は文化の日で祝日になる`` (y: int) =
        DateTime(y, 11, 3)
        |> CultureDay.orNot
        |> isOkWith "文化の日"
        |> should be True

    [<Fact>]
    let ``祝日法の施行以前は、11月3日であっても文化の日の祝日ではない`` () =
        DateTime(1947, 11, 3)
        |> CultureDay.orNot
        |> isOkWith "文化の日"
        |> should be False

module LaborThanksgivingDayTests =
    let afterEstablished: obj[] seq =
        seq { 1948 .. DateTime.Now.Year }
        |> Seq.map (fun y -> [| y |])

    [<Theory>]
    [<MemberData("afterEstablished")>]
    let ``祝日法の施行後、11月23日は勤労感謝の日で祝日になる`` (y: int) =
        DateTime(y, 11, 23)
        |> LaborThanksgivingDay.orNot
        |> isOkWith "勤労感謝の日"
        |> should be True

    [<Fact>]
    let ``祝日法の施行以前は、11月23日であっても勤労感謝の日の祝日ではない`` () =
        DateTime(1947, 11, 23)
        |> LaborThanksgivingDay.orNot
        |> isOkWith "勤労感謝の日"
        |> should be False

module SubstituteTests =
    [<Fact>]
    let ``日曜日が祝日の場合は、次の月曜日が振替休日になる`` () =
        DateTime(2021, 8, 9)
        |> Substitute.orNot
        |> isOkWith "振替休日"
        |> should be True

    [<Fact>]
    let ``日曜日の次に祝日が連続する場合は、祝日の次の平日が振替休日になる`` () =
        DateTime(2020, 5, 6)
        |> Substitute.orNot
        |> isOkWith "振替休日"
        |> should be True

    [<Fact>]
    let ``祝日法の改正前は、日曜日が祝日であっても次の月曜日は祝日にならない`` () =
        DateTime(1973, 2, 12)
        |> Substitute.orNot
        |> isOkWith "振替休日"
        |> should be False

module ImperialCeremonyTests =
    [<Fact>]
    let ``1959年4月10日は、明仁親王の婚礼で祝日になる`` () =
        DateTime(1959, 4, 10)
        |> ImperialCeremony.orNot
        |> isOkWith "明仁親王の結婚の儀"
        |> should be True

    [<Fact>]
    let ``1989年2月24日は、昭和天皇大喪の礼で祝日になる`` () =
        DateTime(1989, 2, 24)
        |> ImperialCeremony.orNot
        |> isOkWith "昭和天皇大喪の礼"
        |> should be True

    [<Fact>]
    let ``1990年11月12日は、平成天皇の即位礼正殿の儀で祝日になる`` () =
        DateTime(1990, 11, 12)
        |> ImperialCeremony.orNot
        |> isOkWith "即位礼正殿の儀"
        |> should be True

    [<Fact>]
    let ``1993年6月9日は、徳仁親王の婚礼で祝日になる`` () =
        DateTime(1993, 6, 9)
        |> ImperialCeremony.orNot
        |> isOkWith "徳仁親王の結婚の儀"
        |> should be True

    [<Fact>]
    let ``2019年5月1日は、令和天皇の即位で祝日になる`` () =
        DateTime(2019, 5, 1)
        |> ImperialCeremony.orNot
        |> isOkWith "令和天皇即位"
        |> should be True

    [<Fact>]
    let ``2019年10月22日は、令和天皇の即位礼正殿の儀で祝日になる`` () =
        DateTime(2019, 10, 22)
        |> ImperialCeremony.orNot
        |> isOkWith "即位礼正殿の儀"
        |> should be True
