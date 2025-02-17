﻿// var test = "x00: 1\r\nx01: 1\r\nx02: 1\r\ny00: 0\r\ny01: 1\r\ny02: 0\r\n\r\nx00 AND y00 -> z00\r\nx01 XOR y01 -> z01\r\nx02 OR y02 -> z02";
// var test = "x00: 1\r\nx01: 0\r\nx02: 1\r\nx03: 1\r\nx04: 0\r\ny00: 1\r\ny01: 1\r\ny02: 1\r\ny03: 1\r\ny04: 1\r\n\r\nntg XOR fgs -> mjb\r\ny02 OR x01 -> tnw\r\nkwq OR kpj -> z05\r\nx00 OR x03 -> fst\r\ntgd XOR rvg -> z01\r\nvdt OR tnw -> bfw\r\nbfw AND frj -> z10\r\nffh OR nrd -> bqk\r\ny00 AND y03 -> djm\r\ny03 OR y00 -> psh\r\nbqk OR frj -> z08\r\ntnw OR fst -> frj\r\ngnj AND tgd -> z11\r\nbfw XOR mjb -> z00\r\nx03 OR x00 -> vdt\r\ngnj AND wpb -> z02\r\nx04 AND y00 -> kjc\r\ndjm OR pbm -> qhw\r\nnrd AND vdt -> hwm\r\nkjc AND fst -> rvg\r\ny04 OR y02 -> fgs\r\ny01 AND x02 -> pbm\r\nntg OR kjc -> kwq\r\npsh XOR fgs -> tgd\r\nqhw XOR tgd -> z09\r\npbm OR djm -> kpj\r\nx03 XOR y03 -> ffh\r\nx00 XOR y04 -> ntg\r\nbfw OR bqk -> z06\r\nnrd XOR fgs -> wpb\r\nfrj XOR qhw -> z04\r\nbqk OR frj -> z07\r\ny03 OR x01 -> nrd\r\nhwm AND bqk -> z03\r\ntgd XOR rvg -> z12\r\ntnw OR pbm -> gnj";
var test = "x00: 1\r\nx01: 0\r\nx02: 1\r\nx03: 1\r\nx04: 0\r\nx05: 0\r\nx06: 1\r\nx07: 1\r\nx08: 0\r\nx09: 1\r\nx10: 1\r\nx11: 1\r\nx12: 1\r\nx13: 1\r\nx14: 0\r\nx15: 1\r\nx16: 1\r\nx17: 0\r\nx18: 1\r\nx19: 1\r\nx20: 1\r\nx21: 1\r\nx22: 0\r\nx23: 1\r\nx24: 1\r\nx25: 0\r\nx26: 1\r\nx27: 0\r\nx28: 1\r\nx29: 0\r\nx30: 1\r\nx31: 1\r\nx32: 0\r\nx33: 0\r\nx34: 1\r\nx35: 0\r\nx36: 1\r\nx37: 1\r\nx38: 1\r\nx39: 0\r\nx40: 0\r\nx41: 0\r\nx42: 1\r\nx43: 0\r\nx44: 1\r\ny00: 1\r\ny01: 0\r\ny02: 0\r\ny03: 1\r\ny04: 1\r\ny05: 0\r\ny06: 0\r\ny07: 0\r\ny08: 0\r\ny09: 0\r\ny10: 0\r\ny11: 1\r\ny12: 0\r\ny13: 0\r\ny14: 0\r\ny15: 0\r\ny16: 0\r\ny17: 0\r\ny18: 0\r\ny19: 0\r\ny20: 1\r\ny21: 0\r\ny22: 0\r\ny23: 0\r\ny24: 0\r\ny25: 1\r\ny26: 0\r\ny27: 0\r\ny28: 0\r\ny29: 1\r\ny30: 1\r\ny31: 1\r\ny32: 0\r\ny33: 1\r\ny34: 0\r\ny35: 1\r\ny36: 0\r\ny37: 1\r\ny38: 0\r\ny39: 0\r\ny40: 0\r\ny41: 1\r\ny42: 1\r\ny43: 1\r\ny44: 1\r\n\r\nsvb XOR fsw -> nwq\r\ny40 XOR x40 -> hsh\r\npjd XOR bsk -> z35\r\ntmt OR dbj -> qcv\r\nfvw AND ndj -> bms\r\ny09 XOR x09 -> cpt\r\nwcj XOR nct -> z33\r\nx20 XOR y20 -> msm\r\nthq AND bmg -> nfh\r\ncjb OR kqr -> z18\r\nx01 XOR y01 -> mtd\r\ny23 AND x23 -> pcq\r\ny11 AND x11 -> fvv\r\ny03 AND x03 -> vmj\r\nvsm OR nqs -> psj\r\npsj XOR rqp -> z10\r\ny06 AND x06 -> gnt\r\ny13 AND x13 -> jrk\r\nnhq XOR vjj -> z39\r\ndqq OR gkj -> ntg\r\nx28 XOR y28 -> hjc\r\nbff AND hsh -> mvd\r\nx18 XOR y18 -> gdw\r\nbqc XOR mdd -> z16\r\ny01 AND x01 -> dsm\r\ny44 AND x44 -> qmh\r\ncbs AND pfr -> sbd\r\nx39 XOR y39 -> nhq\r\nddf AND pvc -> dbj\r\ny37 AND x37 -> shr\r\nrpv AND wpq -> wjv\r\ndtt XOR qgt -> z17\r\ny24 XOR x24 -> jdw\r\npvd OR ctn -> qgt\r\nwcj AND nct -> gkj\r\njvp AND smc -> rrt\r\nx29 AND y29 -> fqs\r\nnwg XOR fsf -> mdb\r\npcq OR mqc -> sjg\r\nkjd OR dwf -> fsw\r\njrk OR rww -> mcw\r\nmkq OR vmf -> fgg\r\nndj XOR fvw -> z19\r\nfdq OR rrt -> phs\r\ngsc OR gnt -> hrn\r\ny08 XOR x08 -> kvn\r\nrjn XOR svf -> z15\r\njqn OR rwg -> rpv\r\nx06 XOR y06 -> rqd\r\nnwg AND fsf -> z22\r\ny27 XOR x27 -> knv\r\ndnn XOR hrd -> z11\r\ny42 AND x42 -> nwp\r\nvmj OR btd -> bqv\r\ny40 AND x40 -> dtp\r\ny12 XOR x12 -> skg\r\nx30 AND y30 -> dmf\r\nbmg XOR thq -> z21\r\nx25 XOR y25 -> smc\r\nsjg AND jdw -> nhj\r\nx15 AND y15 -> spm\r\ny41 AND x41 -> tmt\r\nvmq XOR hmw -> z37\r\ndwq OR tkd -> cbs\r\njjg OR fvv -> hwd\r\nx32 XOR y32 -> npn\r\njpq XOR wjg -> z02\r\nrjb OR kwm -> gfc\r\ny31 AND x31 -> wfh\r\nmdd AND bqc -> ctn\r\nwpq XOR rpv -> z05\r\nx35 XOR y35 -> bsk\r\nmtd AND ppj -> jrp\r\nhrn AND ckm -> vds\r\nx07 XOR y07 -> ckm\r\nx05 XOR y05 -> grf\r\nx07 AND y07 -> hgj\r\nwjv OR grf -> chd\r\nx20 AND y20 -> prv\r\njqg OR dhg -> tpj\r\npws OR bjg -> bff\r\ny24 AND x24 -> rrr\r\ny43 XOR x43 -> vbm\r\nx03 XOR y03 -> hsj\r\nrqd XOR chd -> z06\r\njdw XOR sjg -> z24\r\nffh XOR gdw -> fvw\r\ngjb AND djh -> jqj\r\nhgq XOR fbm -> z30\r\nx21 XOR y21 -> thq\r\ngjb XOR djh -> z38\r\nrmj OR cbr -> pjd\r\ny00 XOR x00 -> z00\r\nnpn AND vrp -> smh\r\nx32 AND y32 -> whh\r\nqcq OR kjq -> fmd\r\nx44 XOR y44 -> pfr\r\nbsk AND pjd -> dwf\r\ndmf OR gcw -> jpb\r\nmtd XOR ppj -> z01\r\nsbs OR mdb -> dqr\r\ny13 XOR x13 -> rnw\r\nhgj OR vds -> wjp\r\nvdd OR wfh -> vrp\r\nx22 XOR y22 -> fsf\r\nx31 XOR y31 -> qjc\r\ny22 AND x22 -> sbs\r\ny36 AND x36 -> z36\r\nbqv AND fdk -> rwg\r\nkvm AND qcv -> krc\r\nhwk OR bqk -> dnn\r\nskg AND hwd -> fbd\r\nftr AND mcw -> fkw\r\nbcc XOR rnw -> z13\r\ny16 AND x16 -> pvd\r\nqcv XOR kvm -> z42\r\nmhf AND phs -> dhg\r\ntpj AND knv -> kjq\r\ny00 AND x00 -> ppj\r\ndcn AND fgg -> wpr\r\nrtt OR nfh -> nwg\r\nshr OR wmg -> gjb\r\nx23 XOR y23 -> bnv\r\ny04 AND x04 -> jqn\r\ny02 XOR x02 -> jpq\r\ny17 AND x17 -> vvt\r\ny27 AND x27 -> qcq\r\nx34 XOR y34 -> htp\r\nnwq OR tvh -> vmq\r\nqvr OR jqj -> vjj\r\nx17 XOR y17 -> dtt\r\nhsj XOR gfc -> z03\r\ny16 XOR x16 -> bqc\r\nnrr AND msm -> vvb\r\nnhj OR rrr -> jvp\r\nmvd OR dtp -> pvc\r\nvmq AND hmw -> wmg\r\nkvn XOR wjp -> z08\r\nx21 AND y21 -> rtt\r\nqgt AND dtt -> bct\r\nx19 AND y19 -> tdg\r\nx41 XOR y41 -> ddf\r\ntvb OR gbn -> bfv\r\npvc XOR ddf -> z41\r\nx05 AND y05 -> wpq\r\nftr XOR mcw -> z14\r\ny19 XOR x19 -> ndj\r\nx26 XOR y26 -> mhf\r\nbff XOR hsh -> z40\r\nx43 AND y43 -> tkd\r\nfsw AND svb -> tvh\r\ntdg OR bms -> nrr\r\nntg XOR htp -> z34\r\nwpr OR fqs -> hgq\r\nvvt OR bct -> ffh\r\ny25 AND x25 -> fdq\r\ndcn XOR fgg -> z29\r\nhtp AND ntg -> cbr\r\nbnv AND dqr -> mqc\r\ny04 XOR x04 -> fdk\r\nprv OR vvb -> bmg\r\ny33 AND x33 -> dqq\r\ny18 AND x18 -> cjb\r\nqjc XOR jpb -> z31\r\njpb AND qjc -> vdd\r\nfmd AND hjc -> mkq\r\nfbd OR rvn -> bcc\r\ncbs XOR pfr -> z44\r\nx34 AND y34 -> rmj\r\nx30 XOR y30 -> fbm\r\nhrn XOR ckm -> z07\r\ndnn AND hrd -> jjg\r\nnqg XOR vbm -> z43\r\ngpc OR fkw -> rjn\r\ny37 XOR x37 -> hmw\r\nhgq AND fbm -> gcw\r\ny02 AND x02 -> rjb\r\nx10 AND y10 -> hwk\r\ny38 AND x38 -> qvr\r\nx14 XOR y14 -> ftr\r\nvjj AND nhq -> pws\r\nnwp OR krc -> nqg\r\nx11 XOR y11 -> hrd\r\nbfv XOR cpt -> z09\r\ngdw AND ffh -> kqr\r\ny26 AND x26 -> jqg\r\nx15 XOR y15 -> svf\r\nx33 XOR y33 -> nct\r\nchd AND rqd -> gsc\r\npfb OR spm -> mdd\r\nnpn XOR vrp -> z32\r\nrjn AND svf -> pfb\r\nvbm AND nqg -> dwq\r\nx12 AND y12 -> rvn\r\nx29 XOR y29 -> dcn\r\nx38 XOR y38 -> djh\r\nskg XOR hwd -> z12\r\ny14 AND x14 -> gpc\r\nqmh OR sbd -> z45\r\nkvn AND wjp -> tvb\r\njvp XOR smc -> z25\r\nfdk XOR bqv -> z04\r\nbfv AND cpt -> nqs\r\ny09 AND x09 -> vsm\r\ny10 XOR x10 -> rqp\r\nsmh OR whh -> wcj\r\nx39 AND y39 -> bjg\r\ny08 AND x08 -> gbn\r\nx36 XOR y36 -> svb\r\npsj AND rqp -> bqk\r\ny35 AND x35 -> kjd\r\nmhf XOR phs -> z26\r\ny28 AND x28 -> vmf\r\ny42 XOR x42 -> kvm\r\nbnv XOR dqr -> z23\r\ngfc AND hsj -> btd\r\nnrr XOR msm -> z20\r\ndsm OR jrp -> wjg\r\nrnw AND bcc -> rww\r\nwjg AND jpq -> kwm\r\nfmd XOR hjc -> z28\r\ntpj XOR knv -> z27";

var solution = new Solution(test);

Console.WriteLine(solution.Solve());