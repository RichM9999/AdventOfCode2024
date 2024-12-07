﻿//https://adventofcode.com/2024/day/3
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Day3
    {
        public void Run()
        {
            var memDump = @"<,:[*where()%mul(53,612)!^}&mul(3,518)??$~select()>??]mul(245,515),why()who()*@from()(where(242,190)mul(817,764)^select(),+who(851,301)where())from(){;mul(431,780)mul(110,982)what()what()]mul(441,829)??where()mul(269,112)>when()?who()<mul(131,147))}]what()^~)mul(186,137)when()'when(443,998)when()+-^what(770,821)?mul(742,949)>$**#!@mul(343,569),;what()from(){(;}mul(486,404)why()]#~when()%@do()+:'why(256,886)why()who(868,710)mul(103,406)?>mul(563,652)from()$-@when())!@:from()mul(744,992)[<^~}mul(822,789)],+select(45,52)!,why()<mul(609,728))who(879,937)&who(770,986):<-mul(723,41)<;select(366,476)why()(who()(%:'do()][@select()how(509,958)%^-<mul(402,767)]how()} #how();don't()how()mul(761,839)!%[#who()who()how()who(),-mul(769,131)>*mul(433,911) }why()<&&how()who()?]mul(877,899);':what())#{]@mul(401,705)@&<how()#mul(970,37)&# #+why()!how()mul(18,953)+what()mul(185,46)'/select()!{mul(474,848):>mul(627,54)? )~+mul(668,930)%)}do()what()#from()$/when()[:when() mul(545,444)how()]<how()^:?+-how()mul(470,601)how()~%from():how()mul(344,452)why()what(266,804)why()how(){who()do()mul(368,423)<#mul(662,80))+mul(7,851)mul(412,165)^,from()'*mul(653,405){}/,from()how()^#mul(592,415);}}>mul(409,150)>?%?@mul(295,205))'mul(321,875)mul(915,728){?when()what(644,532)*?;<<mul(61,812)#select()when()mul(101,342)<<!~why()mul(767,779)~#(^mul(176,413)}-]@>):(:mul(566,595)%'mul(499,468)/'where()}mul(721,340)]when()mul(162,291)mul(73,373)<]where()mul(100,385)][what()]mul(832,226)mul(675,546)who()how()#<^+how()'mul(119,723)what(){+,-mul(959,612)select()mul(758,905)!mul(247,521)&}$?don't()!'$ what()where(353,94)select(){mul(997,311)@from()/mul(987,583)&select(207,730)mul(299,379)select()do()what()select()when()select()]*?(?mul(841,179)!when()what()where()@:why()'>]mul(265,944)mul(968,747)what()}),(mul(541,36)~how()~select()where()when()-+when()mul(448,567)select()where()why(342,599) -when()<}mul(566,357)/$*>> mul(414,962)mul(904,116)what():/'@--}where()mul(390,663)who()~~],when()&where()[%mul(304,146))'who()>+when(){mul(526,627)!;(]@}where()where()mul(253,767)-^+]how()>,,!do():~[^when()]mul(263,450)mul(757,944)><'<mul(919,322)'mul(622,437)@mul(656who()[why()mul(998,113)<who()from()mul(103,114),from()where()/what()((select()'mul(961,712)$<when()select()[{}from(){when()mul(353,694)how()},what()mul(893,557)why()mul(137,971)<why()how()~&<,from()mul(925,982)]$+why()$why()]mul(721,960)[< ,&{when(293,949)select()@mul(788,237)%?+ > (],mul(934,282)@{who()+%when()mul(136,652)mul(366,573)!$//where()mul(501,845)}+~select()+]mul(532,462),~},from()mul(322,350)@~mul(64,374)&what()>[){mul(962,241),?/}-'+mul(39,522)%!$do()^}mul(447,40)mul(971,488)()mul(983,842)where()/mul(956,799);*+)^mul(233,820)mul(957,287)&>[when()<who()]%mul(48,993)# /:&when()]mul(164,671))?+~#;@mul(276,696)how()<+mul(557,793)?/select()mul(644,816)select(375,883)mul(10,640)?(~select()?!%,,'mul-;mul(976,164)*who()[mul(99,562)@why()!<:@mul(678,254)](/from()!]+%+mul(301,985)who()}[who()who()$mul(990,670)-<mul(187,242) mul(643,672)&?;mul(935,319);why()<[/](+((do()+]mul(137,423)*~ #!select(){mul(507,799)~:mul(351,147)
;mul(355; }~-&'mul(929,70)what()where()>why()where(855,333)<@)mul(304,558){~from(),how(){mul(555,718)]]-where() mul(881,781);^&);mul(231,766)$)mul(7,979)^why()'/,mul(352,951)mul(575,449)'mul(803/#(<^when()>mul(500,697)]~^<>[select();^when()mul(822,422)@[where()what() how()&;[(mul(756,744)how() @mul(741,854)what()(do()&how()){what()'^}-mul(664,580)<+ &+mul(788,961) -&}#!,how()who()mul(378,886)[!{,mul(53,236)what())select():~>why()~@who()mul(275,625)*from():what(80,711)why()@don't()mul(426,724)?&from()-)/^-mul(47,60)&+select()?%why()&where()mul(706,388)from()/how()mul(2,761)mul(377,995),from()*?{how()mul(198,840)why()&';[/+^~mul(72,849)mul(720,901 <(mul(998,859)mul(409,24)>?#-)why()mul(208,800)~how()+^?why()^mul(765,394) )mul(269,340)>mul:$~&select();mul(466,453)why()mul(59what()-?'where()?;who()mul(474,555)<[]]when()%-how(225,412)mul(626,574)/,{{what(832,679)where()how()mul(250,300)<#$;+mul(840,193)who(562,102),mul(390,251)(mul(6,397%~what();%who()from()#![mul(95,829)where(811,818)@mul(847,742)'~+&~@/mul(329,961)>~mul(625,917)#+what()<!+why()(how();mul(608,813)}(}how()!when()&+(mul(103,676)>%?>mul(314{mul(392,487)mul(770,684)]'mul(607,73)]where()/#what(378,688) :}'why(778,249)mul(732,423)from()$;@why()>mul(255,618),#+from(849,914);>mul(99,409):]mul(948,905)select()<@mul(342,35)$!@how()/)where():mul(49,617)mul(396,154)>mul(671,367)mul(68,795)~select()<:select()~/mul(900,855){ why(622,665)#when()-<@mul(629~{;~+how(){'mul(907,182)}<;+{' mul(223,519)}+>mul(776,473):where()what()^when()don't()/^ -/(/mul(894,814)mul(698,226)}<mul(188,843)when()mul(933,889)do()}~&%+,:>~/mul(570,960),?@?what()select()}/mul(918,543)'[from()&why()where())~from()[mul(321,507)mul(616,831)why()when()/]{mul#>[!!; mul(480,401)'%%~](from()mul(643,85)who()where()mul(982,510)when():,(mul(799,43[from())$what()%where()@;)mul(420,538)how()mul(358,891)who()what()what()select()(?how()*)mul(612,717)<+(where();who()from(170,645)how()why()where(310,422)mul(789,673)<what(139,371)mul(653,849)(mul(694,267){$(^ !mul(235,885)*>mul(779,49)what()who()[from()$*'&mul(211,472)why()< mul(77,895)?&:*-;^mul(861,148)/what()[+#}when(360,356)mul(905,744):~<-how()][-where()^mul(272,757select()'mul(994,335) what(362,794)$;$mul(242,327)who()when()who();don't()$mul(808,996);don't()/}from()(what()who()mul(398,570)from()~select()[mul(420,441)+mul(452,212)who();~what()#how()what()mul(557when(699,863)mul(184,806)&')from(),:'';don't()[select()mul(139,882~^(how()&mul(931,970)</what()^mul(123,183)select()/^where()when()+#mul(78,494)mul(77,46how(526,601),what(173,89)don't()mul(17,636)why()what()(?do()?what()+when(){;how()why(881,708):mul(424,30)]+mul(137,11)how()where()mul(436,135)from();* %how()-+<mul(682,942+from() from()+where()<%~mul(496,756)^when()-/;</mul(221,864),@,when()mul(451,731)<:why()(mul%<why()from()?~mul(578,746) ]]}!}#mul(392,317)why()](from(685,426)mul(742@:!<mul(348,805))$^mul(157,336)who()$$[{$,)#mul(104,791)what()mul(366,291)?@select()'{from()*mul(528,408)when();'$ ^[:mul(599,79):*;;,from()how(849,837) mul(450,261):<}why() ]@#mul(391,298)<;@why()@,'+,~mul(292,347)mul(306,66) (mul(918,583) @{~from(907,861)'~ ]what()mul(688,890)]what()~ who()when()&?< mul(664,167)>>]] &do()&*)(,:;,mul(924,272)who()$$%select()?/[mul(712,215)~,<from()*?mul(387,653)&mul(915,277)!where(){mul(896,316)what()$mul(60,641)why()mul(190,493)?*{why()+?-mul(516,394)
how()from()*^+from()from()where()}where()mul(109,757){;#mul(809,131)$from()where(43,487)mul(738,564):$*![mul(412#},,^mul(554,33)%why()-mul(617,108)why()mul(528,212)why()from()'!*mul(290,237)-mul(483,362)mul(326,290),who()#(%how()what(){[when()mul(135,76)mul(876,399)!}[^&~from()mul(827,264)%when()~&mul(347,10)mul(315,717)~'where()mul(650,381);,/mul(223,697)}%-*[$mul(296,607)@how()mul(766,686)-]:/when()what()mul(247,762)mul(840,623)$-]mul(879,79)]don't():where(838,375){:(%&when()mul(623,855)@++mul(401,488)!why()mul(713,679)! ?*;/^<%?mul(171,963){$$$~$[@)>mul(114,862) :select()#who()$&how():mul(617,150)]why()~(&@?mul(28,990)-)mul(221,546from()^:<&/[select()mul(5,78)mul(476,316)(<'%[mul(227,163)from()where()who()why()[ mul(229,680)>/+?how()<mul(969,755)!^why()how(965,419)mul(323,258)?>;select()&$[mul(110,33)~{)#who()]^+/mul(479%%#{#from()select()when()what()what()<mul(474,447)'where()mul(643,404)&*)@?('mul(768,724)@)?']>;)//mul(229,348)how()*/mul(643,351)mul(615,362(/->(how()-when()mul(499,697)*don't()mul(992,821){from(413,389));when()who()#mul(921,742)mul(253,580)]how()how(102,372) mul-from()where()mul(104,127)what(),{what() what()[*mul(657,659)'-select()mul(612,851)how()<~-when()$select(718,662)from()(!mulselect()-/^,<&^~mul(497,827)who()-how()-$select()+*mul(943,533)@;mul(403,629)why(826,610)} +:>mul(943,942)why():-)>^why()what()how()mul(302,757'mul(22,286):/-{mul(391,954)mul(528,734)select()-where()why() mul(909,426)!/($+mul(106#&-from()^mul(592,459)'(select()]!when()@{}*mul/&'>mul(894,472)who(),-#mul(924,264)%where()-{from()!mul(956,587)-$do()&why(173,205))$&~where()#&}mul(615,17)how(){>%from()from()@where()mul(683,358),how()when()<!@mul(32,94)!;why()mul(777from()+, :]}why()do()<'mul'>-}](;/mul(688,622)/select()}(what()mul(872,568)/why(){mul(600,562) #mul(733,946)( ?]/when()$(mul(606,986)@!when()mul(597,648)what()-who()from()?,>]<when()mul(18,911)/ how()@,]- {mul(267,646)}do()why()@!;why()mul(322,612),,;?(?^who()mul(34@*+[+^+<how()@#mul(946,367){^,mul(325,609)@#mul(496,574)?~)'*^~when()$'mul(493,377)+#~+>!'mul)>-&@~who()/%!mul(486,264)from())mul(556,968)(who()[:$:,*mul(564,519)# %)&/from(390,158)mul(583,746)how()why()('#/mul(519,835)&(/?^#,mul(447,762){?,]]]/@mul(257,451)&<$why()(mul(670,126){#:mul(555,660)}+%$:(select()mul(723,37)why()who()}where()}how()%)>don't()#where()how()]%what() select()mul(726,542)how()mul:how()/~}</>) mul(753,350)mul(905,645)+ how():(when(879,434)mul(632*?'$/,<*what()mul(442,98)$&(&;mul(424,515)!mul(443,813)**),^%how()what(),mul(252,469)select()[)mul(370,416)%&::&+%mul(885,78)how()^mul(885> {&,why()$mul(52,196)+,{%select(),!##;mul(714,770)mul(607,394)when()how() -@]mul+^@mul(90,350)~@+where()mul(934,150)when(788,941)^mul(557,327)mul(86,867) :~$-how()select()who()mul(166,855) what() %]mul(928,22)%when()-how()$!what():mul(628,252)~{>,who()~where(967,401)#mul(943,366)'[*>@mul(781,986)>:when()?&don't()~ :where()who()<what()who()mul(645,747)$,({$[mul(75,880),$?:when()what()[!when()mul(617,589)how()from()why()what()from()mul(63,146)'from()who()mul(530,247)!mul(747,431),(mul(920,122)mul(206,825)+when(414,126)>select():what()mul(839,261) *&*where())/]when()mul(24,536)how()how()'who()why())<,~mul(254,257)^@:where()mul(945,657)!&]select()from()}'[#)mul(148,151)-#!/usr/bin/perlwhere()>%why()}!/]{;mul(549,60)
how(477,669) mul(853,888)<[/who()>}+mul(361,751)mul(837,651)-mul(8,515)(}>!!# do()&@!:^][@mul(143,286)&?^&*how()&!'mul(88,781):}who(){!*!&mul(348,435)mulfrom()?>]-}+ }@mul(33,697)do()what()why()}(#]:mul(3,176):$mul(613,679),'how(882,581)who()*mul(519,798)from()mul(865,503),+~mul(767,837)/)?^why()/mul(520,288)where(){)who()what(); )mul(274,562)$:<^*what(423,612) from()>)mul(375,282)#select()from(646,967)?(*!'$mul(991,635)mul(598,876)%^?+mul(716,646)!)&mul(699,500){^^who()when()from()what()mul(314,198)%@;mul(419,800from()]$mul(844,419)$&]mul(317,754))<;^@(mul(222,12)##:what(751,642)$[from()%;mul(205,32)when()&{>+'>why()+mul(864,418)*@select()mul(911,629)*why();)^who()mul(248,4)}mul(132,503)/when()why()%mul(441,482)-mul(818,349)(#(where();(who(22,952)mul(534,621)$'~]where()why()-mul(199,973)mul(746,506)@'select()%{mul(866,951)from()mul(381,827)[select()*}(what()mul(744,979)mul(907,379)how(347,456)who()@what()##>;@mul(114,609),}what()'mul(406,863)][who()/when()mul(900,454)<'-)->[select()!select()mul(499,399)who()mul(349,409)',-~)select()mul(234,866)'why()mul(225,989)-(-mul(626,378);,$who()mul(597,85)mul(977,267)select(){mul(111,206):(&mul(989,396)#[:)<~{who()select()mul(689,817)mul(240,827)#how(){-+-@mul(663,156)'^$from())mul(416,908)*%why(714,896)#^+mul(993,707)'when()#;who()>where()select()?mul(978,206)@how(315,762)*/>why()({mul(998,362)who()$<(mul(488,946)},when()):}?where()where()}do()[!;?~{select()mul(673,574)%[::@;why()#]+mul(455,366)$~:mul(351,347)%,~-?what(595,890)]*select()mul(186,325)where()/:why(744,171)mul(513,868)/+why(118,201)mul(691,608) %mul(469,265)why()do()[mul(80,813)*<:mul(925,31):((mul(640,344)when(758,885)where()mul(474,482)?[>{where()]!mul(66,500)why()![# -mul(549,187,how()'where()*don't()#-<>%select()?who()]mul(581,943)?]what()}?) mul(54,791)]do()+$-)mul(255,793)who()]mul(338[)how()!@mul(253,351)++who()$mul(696,214)select()&mul(899%what()~}/what()-mul(302,687)when()#&mul(842,464)}?^$when()([()mul(579,190)<mul(432,910)^)mul(9,997)who()::what()from()mul(480,244)&)#mul(574)$@-]+$;mul(550,817)why()]<}[}who())]mul(129,159#$(?when()who()$^ mul(266,573)@mul(624,390)what();select()*from()mul(22,674$:]mul(675,762)(why(){://mul(493,757)*,$mul(624,120)?how()where()}mul(225,47):@)why()select(378,744)%& mul(867,37)~how()mul(91,26)$@]%){#select()*@mul(138,170)?*mul(531,483)-+<+)mul(714$who()select(569,250)}{)how()don't()?-when()when()-^%+^mul(475,925)why();!:from(){why()why()mul(231,576)'~@why()who(681,216)%(,/when()mul(393,115)^>@mulwho()where()~mul(238,298)from()<^(+,mul(766,436)how()^)%':{mul(427,646) ?%}(?<mul(476,338)from()why()&-[;'what()~mul(724,149~mul(25,405)mul(107,418){select()mul(107,999){why();]]mul(13,404)&^<@mulwhen()->mul(995,814)#~where()'}mul(267,274)']mul(174,583[,((!what()+when()*)mul(146,807)!:where()+!mul(856,39)?;@!mul(653,577)%}^do()^mul(961,701)::mul(23,391)<where()(from()~mul(915,472),& ?<select() when()mul(3,160),]select()?mulselect(),+from()]who()}]where(728,127)}:mul(742,873))mul(419,682)~/;#mul(330,468)[^?#*@~where()^/mul(352,725)>what()^*~-do()how()how(832,138);!}($#+mul(841,486)how()+# mul(255,21))-$who()when() -)> mul(427,236);&who(),where()+{mul(97,633)
::<where()mul(633,320)]([)what()why()^);'mul(695,733);<%how()-select()(;[mul(284,802)&?;when(372,205)mul(860,681),why()how()select(77,254)!;mul(284,605)?mul(312,805)when()how()#mul(256,855)-:#mul(252,605)who()mul(188,300)&-#<from()@where()^why()mul(696,379){how()~;when())mul(202,912)what()how()how(){](:-,mul(889,648),)[-when()!,mul(887,787)%;~{mul(559,711){&mul(664,834)#@where())+>where(946,25)!where()do()?where()-why()mul(129,728)>^&mul(557,253)mul(535,49)who()~$><?!mul(950,144)select(399,41)'':[when()mul(984,990)%/:select()~'^'@-don't() )mul(862,540)from()]who(964,165)don't()from()mul(378,581)[[from(295,573)~]/!)mul(29,139)mul(992,600))mul(560,53)>mul(342,811)([*don't(),+how(398,338)}why()<:mul(556,398)[@??:^(),how()mul(708,821):;-?</where()mul(160,2):when()select()mul(870,629))^]&when()^select(859,207)don't():{who()who(29,837)select()'}}who()~mul(392,813)'mul(145,581)mul(805,802)what()$]@+who()/do()#&how():select()])mul(472,752)>where()/when()select()%?how(955,77)why()mul(428,310)how()where()'mul(759,360)(#>;where()when()mul(396,975)}{mul(131,376)(!@what()%] @mul(100,818)mul(478,394)<%$<:%($&/mul(337,545)mul(227,220)who()-'+why()select()select()don't()why():';!mul(49,915)+))[do()?[when()mul(886,984)'^ who()>when()~mul(468,20)from()(<>why(){mul(791,347)? &} +mul(389,748)/[<;when()select()*mul(370,770)::[select();what()mul(63,701)why()^mul(361,364)when(592,110)select()+: ]mul(988,308)$mul(693{from()!(how()~/mul(782,22)why()mul(355,443)}<)%how()how(){[mul(47,507,^}>)&#don't()why()mul(459,418)?who()/(how()select()when(456,391)?select()mul(451(mul(473,946))who()how()*<how()+!@what()mul(228,381)mul(800,750)how()')<+)from(),when()'mul(292,596)*select()why():what(158,611)(,who()#mul(825,578)mul(990,239)!^~,$>mul(733,242)mul(575,863)why()*>&,}%!]^mul(613,249){#from()mul&]who()where()}+}}$+mul(94,794)mul(813,82)#mul(294,897)mul(995,245)!%#!^why()mul(411,763)/}</where()who()when()mul(629,353)<]when():do()*mul(657,319)**?select()^&%(<mul(900,780'~how():who()^when()<*mul(685,253)-<mul(465,59)&mul(570,657)-&mul(314,390){@what()%mul(730,685)}from()when()&],<}mul(952,18)what()/;?#^?from()mul(214!/'],(]don't()%?+]-?^^-&mul(108,152)&~(]do();:(mul(27,545)<how()how()how()*mul(20,169)&~how()how()when()@mul(679,797)[how(),/mul(744,612),mul(270,322)mul(803,691);{select(427,50)mul(805,251)'?}mul(308,255)[[!^!who(){<;where()mul(817,821)/<who() where(698,784)$when(867,773)]mul{>what(),)mul(952,776)<mul(215,986)<mul(617,955)?^,[@('where()mul(984{,~#)-how()mul(758,488)$mul(87,191)from()/;}from(595,784)!when()mul(284,414):*>mul(868,728) ':~$}mul(869,784#who()don't()%@-} (mul(766,260)']who()@^&select()<when()<mul(812,773)%from()who()why()[:^mul(998,763)when()-mul(34,331)where()who()/]]mul(854,2)do()#what()mul(342,466)+$@ ?,who()*mul(92,63)#'from()who()~;when();where()?mul(175,64)+;mul(128,996)?from()mul(123,206)select()>mul(723,930)what()what()from()-#~!mul(873,766)%:,?mul(520,521)& mul(407,520)select()$mul(547,416)~?}%^mul(677,996)mul(661,622)[~!mul(310,928)^;>-mul(740,171) /$#$*don't()select()>:  &mul(306,628)*{select()$mul(705,686$/mul(803,660)mul(485,760)[select()'select(){@mul(917,878)#mul(188,324)(>}<:select()'mul(281,982)(select()},what()mul(730,931)}-#mulwhat(469,510)select()'from()where()#from()#do()/mul(869,458)
when()from()mul(912+{@mul(635,991)>}mul(158,871){[*{>'$*/ mul(686,481)mul(339,86)$>how()[when()mul(78,204)+&'mul(58,734)<{who()where()]';~mul(964,213)&select()where()@where(513,771)mul(471,186)'%$)@~<how()%how()mul(363;(]>({# mul(517,787){+%from()>]{,mul(762,604),~from()~+do()select()mul(827,656)$^>['-what()mul(318,236)select()'+>who(530,155)*mul(394,518)&#)select()from()!)select();:mul(805,79)why()}how()/&<mul(199,198)+mul(206,829)who()^}select()(]mul(216,978)@why()from()mul(129,689))~/,?where()mul(894,847)select() /#(]*mul(145what(119,568)mul(605,74)select() <when(246,601)-what();mul(642,999)@/;}+from():mul(423,41)mul@<+mul(90,853)what(565,411)![who():mul(971,380))who()mul(303,454)?#$#-'-do()who()]@}why()?!>?mul(924,569)who()$%where()]~*&[why(621,535)mul(152,407)[why()]#<!*select()#[mul(486,227who()*?)<where()%}mul(920,490)<$)#*$#mul(987,463)~?who()from()<(,mul(92,447)how():who()<+&why()>mul(478,24)mul(107,697)<why()who()%/>select()'>~mul(481,432) mul(248,61)^:+don't()mul(400,374)^]{who(){mul(959,329)from(){))]mul(566,182)}what()(where()>from()&don't()select()who():+?!]]/mul(766,741)why()@ !how()who()?select();<mul(434,357)%from()what()'^:!]when()?mul(798,701)#{,!:select()@what()%~do()]what() $&from():(&*mul(875,691)?%why()&[select();[/]mul(665,14)~:$*%mul(841,909)!<</#why()!]how(153,944)>mul(470,17)/mul(374,487)%@@mul(685,185)} {<#(mul(845,259)'why(230,429)[??^select()^>/mul(791,324)$how()/?mul(478,641)+/ <{(when()mul(794,547)why()[select()!*-mul(474,709)#select()# select()}select()mul(896,775)select()-where():!mul(817,123)mul(333,359)mul(433,461)mul(722,474)@'!@$mul(49,892) #(+,mul(726,435)what()>,;mul(957,798)) #%)$ mul(541,867)-?*# do() -]who(265,168){'*>{mul(770,902)+,-who()*what()mul(21,402)mul(843,622)~#mul(604,248)how()%(why()where()[;where()+mul(342,802))from()*when())#,<#,mul(439,954)}@-&)>+what()mul(593,520)>?,what();+,select()~mul(202,526)where()+#-]? mul(654,204)>why(),(]+mul(95,133)mul(626,152)@,[+mul(811,855)when()$when(387,144)when()who()mul(781,279))+&@&mul(77,160),why()%from()do())#mul(982,99):mul(722,476)mul(757,193)from()when()select()who(){mul(569,549)]*$mul(943,277)why()@who()}{from()!when()]mul(714,885),;*do()@where()^how()mul(941,709)~@{when(215,138)how()mul(772,310)~,what()>who()!?++]mul(858,573)-what()<(from()from()mul(254,457)mul(890,367)from()why()[mul(590,183)/@who()*#-^?~mul(446,447)<how()-)<}/how()mul(737,533),{>how()$select()mul(734,361)]~where();}when()mul(50,364):(where()-<^why()!&mul(692,682)from())&mul(949,43)mul(923,776)^/mul(987,377)!who()mul(286,329)#what()~from()^who()#/mul(13,658)why()>@(>]mul(592,423)mul(148,486)*mul(878,721)$%#how()what()mul(710,18):[#%do()where()@!(~mul(357,115)where()select()how()don't()mul(356,206)@,? how()^^what(295,729)[mul(759,861)from()*/select(334,18)&*how()@>what()mul(580,828)+;where()^;*select();what()mul(542,530)~where()[mul(549,51)'!mul(12,992)#why()don't()!;how()-(+{(mul(543,363)[what()from():mul(135,364)/select()mul(312,500)@where()[when()&$)]when(610,72)mul(215,533)~;who()}how()from()*)>~mul(522,58)@mul(402,510)!!;who()mul(867,26)-mul(25,987)?select()mul(973,76)why()&#{(/mul(148,345))^'%@when()-how()mul(233,85)";

            var result = 0;

            Regex test = new(@"mul\((\d{1,3}),(\d{1,3})\)|do\(\)|don't\(\)");

            foreach (Match match in test.Matches(memDump))
            {
                int.TryParse(match.Groups[1].Value, out int first);
                int.TryParse(match.Groups[2].Value, out int second);
                result += first * second;
            }

            Console.WriteLine($"Sum of all mul's: {result}");

            var enable = true;
            result = 0;
            foreach (Match match in test.Matches(memDump))
            {
                if (match.Value == "do()")
                {
                    enable = true;
                    continue;
                }

                if (match.Value == "don't()")
                {
                    enable = false;
                    continue;
                }

                if (enable)
                {
                    int.TryParse(match.Groups[1].Value, out int first);
                    int.TryParse(match.Groups[2].Value, out int second);
                    result += first * second;
                }
            }

            Console.WriteLine($"Sum of all enabled mul's: {result}");
        }
    }
}