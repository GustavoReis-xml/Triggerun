using System;
using System.Collections.Generic;

// Token: 0x02000255 RID: 597
public class NetMsg
{
	// Token: 0x040015FB RID: 5627
	public const int LOGIN_MSG_ACTION = 1001;

	// Token: 0x040015FC RID: 5628
	public const int LOGIN_MSG_CREATEROLE = 1002;

	// Token: 0x040015FD RID: 5629
	public const int LOGIN_MSG_LOGINMULTI = 1006;

	// Token: 0x040015FE RID: 5630
	public const int LOGIN_ACT_NICKERROR = 1016;

	// Token: 0x040015FF RID: 5631
	public const int LOGIN_ACT_CREATEERROR = 1017;

	// Token: 0x04001600 RID: 5632
	public const int LOGIN_ACT_CREATEOK = 1018;

	// Token: 0x04001601 RID: 5633
	public const int GATE_MSG_CHECKONLINE = 1119;

	// Token: 0x04001602 RID: 5634
	public const int GATE_MSG_LOGINOK = 1120;

	// Token: 0x04001603 RID: 5635
	public const int GATE_MSG_USERLOST = 1121;

	// Token: 0x04001604 RID: 5636
	public const int HALLGATE_MSG_FINDUSER = 1205;

	// Token: 0x04001605 RID: 5637
	public const int HALL_MSG_GETDATA = 1301;

	// Token: 0x04001606 RID: 5638
	public const int HALL_MSG_HALLMULTI = 1302;

	// Token: 0x04001607 RID: 5639
	public const int HALL_MSG_CHGSCN = 1304;

	// Token: 0x04001608 RID: 5640
	public const int HALL_MSG_HEROUNLOCK = 1305;

	// Token: 0x04001609 RID: 5641
	public const int HALL_MSG_HERODATA = 1306;

	// Token: 0x0400160A RID: 5642
	public const int HALL_MSG_FCMDATA = 1307;

	// Token: 0x0400160B RID: 5643
	public const int HALL_MSG_TOWEB = 1308;

	// Token: 0x0400160C RID: 5644
	public const int HALL_MSG_LOGINTIME = 1309;

	// Token: 0x0400160D RID: 5645
	public const int HALL_MSG_GMACTION = 1310;

	// Token: 0x0400160E RID: 5646
	public const int HALL_MSG_ROOMINVITE = 1311;

	// Token: 0x0400160F RID: 5647
	public const int HALL_MSG_ROOMINVITEBACK = 1312;

	// Token: 0x04001610 RID: 5648
	public const int HALL_MSG_SHOWJIAOXUE = 1313;

	// Token: 0x04001611 RID: 5649
	public const int HALL_MSG_WENJUAN = 1314;

	// Token: 0x04001612 RID: 5650
	public const int HALL_MSG_LIBAO = 1315;

	// Token: 0x04001613 RID: 5651
	public const int HALL_MSG_CDK = 1316;

	// Token: 0x04001614 RID: 5652
	public const int HALL_MSG_SERVER_CLOSE = 1318;

	// Token: 0x04001615 RID: 5653
	public const int HALL_MSG_DTSMSG = 1319;

	// Token: 0x04001616 RID: 5654
	public const int LOGOFF_MSG_SENDDATA = 1501;

	// Token: 0x04001617 RID: 5655
	public const int SVR_WEB_FTUSER = 4002;

	// Token: 0x04001618 RID: 5656
	public const int SVR_WEB_FENGHAO = 4012;

	// Token: 0x04001619 RID: 5657
	public const int SVR_WEB_UDIDMAC = 4013;

	// Token: 0x0400161A RID: 5658
	public const int QQLUOPAN_QUIT = 1600;

	// Token: 0x0400161B RID: 5659
	public const int QQLUOPAN_CONSUM = 1601;

	// Token: 0x0400161C RID: 5660
	public const int QQLUOPAN_LOGIN = 1602;

	// Token: 0x0400161D RID: 5661
	public const int QQLUOPAN_ONLINE = 1603;

	// Token: 0x0400161E RID: 5662
	public const int QQLUOPAN_JOINLOG = 1604;

	// Token: 0x0400161F RID: 5663
	public const int QQLUOPAN_HITBTUUON = 1605;

	// Token: 0x04001620 RID: 5664
	public const int QQLUOPAN_CREATEUI = 1606;

	// Token: 0x04001621 RID: 5665
	public const int QQLUOPAN_CLOSEWINDOW = 1607;

	// Token: 0x04001622 RID: 5666
	public const int QQLOGTIMEOVER = 1608;

	// Token: 0x04001623 RID: 5667
	public const int LOGDB_MSG_PLAYRR = 2110;

	// Token: 0x04001624 RID: 5668
	public const int SVR_REV_ROOMMSG = 20013;

	// Token: 0x04001625 RID: 5669
	public const int ROOM_MSG_CREATEROOM = 3301;

	// Token: 0x04001626 RID: 5670
	public const int ROOM_MSG_EXITROOM = 3302;

	// Token: 0x04001627 RID: 5671
	public const int ROOM_MSG_ENTERROOM = 3303;

	// Token: 0x04001628 RID: 5672
	public const int ROOM_MSG_LEAVEROOM = 3304;

	// Token: 0x04001629 RID: 5673
	public const int ROOM_MSG_GETPAGEINFO = 3306;

	// Token: 0x0400162A RID: 5674
	public const int ROOM_MSG_UPDATEUSERINFO = 3308;

	// Token: 0x0400162B RID: 5675
	public const int ROOM_MSG_GAMESTART = 3311;

	// Token: 0x0400162C RID: 5676
	public const int ROOM_MSG_KICKUSER = 3313;

	// Token: 0x0400162D RID: 5677
	public const int ROOM_MSG_CHANGETEAM = 3318;

	// Token: 0x0400162E RID: 5678
	public const int ROOM_MSG_CHOOSEHERO = 3320;

	// Token: 0x0400162F RID: 5679
	public const int ROOM_MSG_CLICKHERO = 3321;

	// Token: 0x04001630 RID: 5680
	public const int ROOM_MSG_SETROOMINFO = 3324;

	// Token: 0x04001631 RID: 5681
	public const int ROOM_MSG_GETSOMEROOMINFO = 3325;

	// Token: 0x04001632 RID: 5682
	public const int ROOM_MSG_QUICKJOIN = 3326;

	// Token: 0x04001633 RID: 5683
	public const int ROOM_MSG_CHANGETEAMUSERDATA = 3329;

	// Token: 0x04001634 RID: 5684
	public const int ROOM_MSG_FBLEAVEGAME = 3331;

	// Token: 0x04001635 RID: 5685
	public const int ROOM_MSG_GAMESTARTERR = 3333;

	// Token: 0x04001636 RID: 5686
	public const int ROOM_MSG_SETPLAYERINFO = 3335;

	// Token: 0x04001637 RID: 5687
	public const int ROOM_MSG_GETGROUPLVL = 3337;

	// Token: 0x04001638 RID: 5688
	public const int ROOM_MSG_TEAMJOIN = 3338;

	// Token: 0x04001639 RID: 5689
	public const int ROOM_MSG_MATHCJOIN = 3336;

	// Token: 0x0400163A RID: 5690
	public const int SVR_WEB_WEIHUREWARD = 4009;

	// Token: 0x0400163B RID: 5691
	public const int SVR_FBNETMSG = 5000;

	// Token: 0x0400163C RID: 5692
	public const int SVR_FBPOINTFIGHT = 5001;

	// Token: 0x0400163D RID: 5693
	public const int SVR_FBGAMESTART = 5002;

	// Token: 0x0400163E RID: 5694
	public const int SVR_FBONEGAME = 5003;

	// Token: 0x0400163F RID: 5695
	public const int SVR_FBTWOGAME = 5004;

	// Token: 0x04001640 RID: 5696
	public const int SVR_FBOVERGAME = 5005;

	// Token: 0x04001641 RID: 5697
	public const int SVR_FBGAMEOVER = 5006;

	// Token: 0x04001642 RID: 5698
	public const int SVR_FBMAPEFFECT = 5007;

	// Token: 0x04001643 RID: 5699
	public const int SVR_FBSTRONGSCR = 5008;

	// Token: 0x04001644 RID: 5700
	public const int SVR_CHANEHERO = 5009;

	// Token: 0x04001645 RID: 5701
	public const int SVR_CHANEHEROOK = 5010;

	// Token: 0x04001646 RID: 5702
	public const int SVR_REFRECHHERO = 5011;

	// Token: 0x04001647 RID: 5703
	public const int SVR_CARDIS = 5013;

	// Token: 0x04001648 RID: 5704
	public const int SVR_CARUSERNUM = 5014;

	// Token: 0x04001649 RID: 5705
	public const int SVR_CARPOINT = 5015;

	// Token: 0x0400164A RID: 5706
	public const int SVR_CARINFO = 5016;

	// Token: 0x0400164B RID: 5707
	public const int SVR_CARSTATE = 5017;

	// Token: 0x0400164C RID: 5708
	public const int SVR_ROUNDEND = 5018;

	// Token: 0x0400164D RID: 5709
	public const int SVR_UPDATESTATE = 5019;

	// Token: 0x0400164E RID: 5710
	public const int SVR_ROUNDSTART = 5020;

	// Token: 0x0400164F RID: 5711
	public const int SVR_ADDTIMEUPDATE = 5021;

	// Token: 0x04001650 RID: 5712
	public const int SVR_SCRLLUPDATE = 5022;

	// Token: 0x04001651 RID: 5713
	public const int SVR_SCRUPDATEDATA = 5023;

	// Token: 0x04001652 RID: 5714
	public const int SVR_TIMESEND = 5024;

	// Token: 0x04001653 RID: 5715
	public const int SVR_TABGETDATA = 5025;

	// Token: 0x04001654 RID: 5716
	public const int SVR_GAMEOVER = 5026;

	// Token: 0x04001655 RID: 5717
	public const int SVR_BIGSCROLLPOINT = 5027;

	// Token: 0x04001656 RID: 5718
	public const int SVR_STARTIME = 5028;

	// Token: 0x04001657 RID: 5719
	public const int SVR_EXITFB = 5029;

	// Token: 0x04001658 RID: 5720
	public const int SVR_ENTERTRAINING = 5030;

	// Token: 0x04001659 RID: 5721
	public const int SVR_EXITTEM = 5031;

	// Token: 0x0400165A RID: 5722
	public const int SVR_ENTERLEARN = 5032;

	// Token: 0x0400165B RID: 5723
	public const int SVR_LEARNSETHP = 5033;

	// Token: 0x0400165C RID: 5724
	public const int SVR_LEARNFILLPOWER = 5034;

	// Token: 0x0400165D RID: 5725
	public const int SVR_TEAMLEAVEGAME = 5035;

	// Token: 0x0400165E RID: 5726
	public const int SVR_KICKGAME = 5037;

	// Token: 0x0400165F RID: 5727
	public const int SVR_CHANGETEAM = 5038;

	// Token: 0x04001660 RID: 5728
	public const int SVR_CLOSELINK = 5039;

	// Token: 0x04001661 RID: 5729
	public const int SVR_FPS_PING = 9001;

	// Token: 0x04001662 RID: 5730
	public const int SVR_JOINLOG = 9002;

	// Token: 0x04001663 RID: 5731
	public const int SVR_GETJOINLOG = 9003;

	// Token: 0x04001664 RID: 5732
	public const int SVR_CREATEMODELTIME = 9004;

	// Token: 0x04001665 RID: 5733
	public const int ROOM_MSG_GETROOMINFO = 3307;

	// Token: 0x04001666 RID: 5734
	public const int ROOM_MSG_GETROOMID = 3310;

	// Token: 0x04001667 RID: 5735
	public const int CLT_MSG_GETCOMDATA = 10001;

	// Token: 0x04001668 RID: 5736
	public const int CLT_MSG_REVIVE = 10022;

	// Token: 0x04001669 RID: 5737
	public const int CLT_MSG_FIGHTSTAT = 10023;

	// Token: 0x0400166A RID: 5738
	public const int CLT_MSG_MONEYMSG = 10025;

	// Token: 0x0400166B RID: 5739
	public const int CLT_MSG_MATCHUSERINFO = 10094;

	// Token: 0x0400166C RID: 5740
	public const int CLT_MSG_PAINTSYC = 10095;

	// Token: 0x0400166D RID: 5741
	public const int CLT_MSG_CHANGENICK = 10096;

	// Token: 0x0400166E RID: 5742
	public const int CLT_MSG_RESETCHGNICK = 10097;

	// Token: 0x0400166F RID: 5743
	public const int ZYL_ON_OFF = 0;

	// Token: 0x04001670 RID: 5744
	public const int YX_ON_OFF = 1;

	// Token: 0x04001671 RID: 5745
	public const int SYFW_ON_OFF = 2;

	// Token: 0x04001672 RID: 5746
	public const int HMPZ_ON_OFF = 3;

	// Token: 0x04001673 RID: 5747
	public const int QYSY_ON_OFF = 4;

	// Token: 0x04001674 RID: 5748
	public const int DKXG_ON_OFF = 5;

	// Token: 0x04001675 RID: 5749
	public const int SHTX_ON_OFF = 6;

	// Token: 0x04001676 RID: 5750
	public const int SHPF_ON_OFF = 7;

	// Token: 0x04001677 RID: 5751
	public const int SVR_REV_GOTOHALL = 20001;

	// Token: 0x04001678 RID: 5752
	public const int SVR_RETURNHALLLOG = 20002;

	// Token: 0x04001679 RID: 5753
	public const int SVR_REV_TIMEUPDATE = 20058;

	// Token: 0x0400167A RID: 5754
	public const int PLAYER_MSG = 21000;

	// Token: 0x0400167B RID: 5755
	public const int PLAYER_REVIVE = 21001;

	// Token: 0x0400167C RID: 5756
	public const int PLAYER_ADDEXP = 21002;

	// Token: 0x0400167D RID: 5757
	public const int MSG_WEAPON_SKILL = 31000;

	// Token: 0x0400167E RID: 5758
	public const int MSG_PLAYERSKILL_INIT = 31101;

	// Token: 0x0400167F RID: 5759
	public const int MSG_PLAYERSKILL_UPDATE = 31102;

	// Token: 0x04001680 RID: 5760
	public const int MSG_PLAYERSKILL_DO = 31103;

	// Token: 0x04001681 RID: 5761
	public const int MSG_PLAYERSKILL_CDEND = 31104;

	// Token: 0x04001682 RID: 5762
	public const int MSG_PLAYERSKILL_USETIME_RESET = 31105;

	// Token: 0x04001683 RID: 5763
	public const int MSG_PLAYERSKILL_WP_REPAIR = 31201;

	// Token: 0x04001684 RID: 5764
	public const int MSG_PLAYERSKILL_WP_MAX = 31202;

	// Token: 0x04001685 RID: 5765
	public const int MSG_PLAYERSKILL_WP_FILL_CARTRIDGE = 31203;

	// Token: 0x04001686 RID: 5766
	public const int MSG_PLAYERSKILL_WP_FILL_CARTRIDGE_CANCLE = 31204;

	// Token: 0x04001687 RID: 5767
	public const int MSG_PLAYERSKILL_WP_BOOM = 31205;

	// Token: 0x04001688 RID: 5768
	public const int MSG_PLAYERSKILL_WP_BULLET_FILL = 31206;

	// Token: 0x04001689 RID: 5769
	public const int MSG_PLAYERSKILL_WP_CHANGE = 31207;

	// Token: 0x0400168A RID: 5770
	public const int MSG_PLAYERSKILL_SUPPERSKILL = 31208;

	// Token: 0x0400168B RID: 5771
	public const int MSG_PLAYERSKILL_SUPPERSKILL_DATA = 31209;

	// Token: 0x0400168C RID: 5772
	public const int MSG_PLAYERSKILL_XT_CHANGE = 31210;

	// Token: 0x0400168D RID: 5773
	public const int MSG_PLAYERSKILL_SUPPERSKILLEND = 31211;

	// Token: 0x0400168E RID: 5774
	public const int MSG_PLAYERSKILL_UPDATEITEMNUM = 31212;

	// Token: 0x0400168F RID: 5775
	public const int MSG_PLAYERSKILL_SUPPERSKILLSTART = 31213;

	// Token: 0x04001690 RID: 5776
	public const int MSG_PLAYERSKILL_ROUNDENDMSG = 31215;

	// Token: 0x04001691 RID: 5777
	public const int MSG_ROBOTSKILL_SUPPERSKILL = 31216;

	// Token: 0x04001692 RID: 5778
	public const int MSG_WEAPON_DAMAGE = 31301;

	// Token: 0x04001693 RID: 5779
	public const int MSG_WEAPON_DAMAGE_SMG = 31302;

	// Token: 0x04001694 RID: 5780
	public const int MSG_SKILLS_PARM = 31401;

	// Token: 0x04001695 RID: 5781
	public const int MSG_PLAYERSKILL_WP_NOMOVE = 3100007;

	// Token: 0x04001696 RID: 5782
	public const int MSG_PLAYERSKILL_WP_INVISIBLE = 3100008;

	// Token: 0x04001697 RID: 5783
	public const int SVR_EFFECT_CHECKPLAYER = 40001;

	// Token: 0x04001698 RID: 5784
	public const int SVR_EFFECT_TOG_SELECT_SEARCH = 40002;

	// Token: 0x04001699 RID: 5785
	public const int SVR_EFFECT_CHECKPLAYER_OK = 40003;

	// Token: 0x0400169A RID: 5786
	public const int SVR_EFFECT_CHECKPLAYER_CANCEL = 40004;

	// Token: 0x0400169B RID: 5787
	public const int SVR_EFFOBJ_ADD = 41001;

	// Token: 0x0400169C RID: 5788
	public const int SVR_EFFOBJ_REMOVE = 41002;

	// Token: 0x0400169D RID: 5789
	public const int SVR_EFFOBJ_WEAPON_ADD = 41003;

	// Token: 0x0400169E RID: 5790
	public const int SVR_EFFBUFF_REMOVE = 41004;

	// Token: 0x0400169F RID: 5791
	public const int SVR_MAPEFF_ADD = 41101;

	// Token: 0x040016A0 RID: 5792
	public const int SVR_LOADTEST = 41111;

	// Token: 0x040016A1 RID: 5793
	public const int MSG_DAZHAO = 50001;

	// Token: 0x040016A2 RID: 5794
	public const int MSG_DAZHAO_TOCLIENT = 50002;

	// Token: 0x040016A3 RID: 5795
	public const int MSG_DAZHAO_CHANGEMAIN = 50003;

	// Token: 0x040016A4 RID: 5796
	public const int MSG_DAZHAO_ENERGY = 50004;

	// Token: 0x040016A5 RID: 5797
	public const int MSG_DAZHAO_UPDATE = 50005;

	// Token: 0x040016A6 RID: 5798
	public const int MSG_DAZHAO_ENERGYTEST = 50006;

	// Token: 0x040016A7 RID: 5799
	public const int MSG_DAZHAO_LEIZHENZI = 50007;

	// Token: 0x040016A8 RID: 5800
	public const int MSG_DAZHAO_START = 50008;

	// Token: 0x040016A9 RID: 5801
	public const int MSG_BUFF = 51001;

	// Token: 0x040016AA RID: 5802
	public const int MSG_BUFF_CREATE = 51002;

	// Token: 0x040016AB RID: 5803
	public const int MSG_BUFF_DELETE = 51003;

	// Token: 0x040016AC RID: 5804
	public const int MSG_MAPBUFF_TRIGGER = 51004;

	// Token: 0x040016AD RID: 5805
	public const int MSG_MAPBUFF_EXIT = 51005;

	// Token: 0x040016AE RID: 5806
	public const int MSG_MAPJIDICHECK = 51006;

	// Token: 0x040016AF RID: 5807
	public const int MSG_JUDIAN_ENTER = 52001;

	// Token: 0x040016B0 RID: 5808
	public const int MSG_JUDIAN_SIGN = 52002;

	// Token: 0x040016B1 RID: 5809
	public const int SYSTEM_ARENA = 110000;

	// Token: 0x040016B2 RID: 5810
	public const int SEND_ARENA_CREATE = 110001;

	// Token: 0x040016B3 RID: 5811
	public const int SEND_ARENA_DATA = 110002;

	// Token: 0x040016B4 RID: 5812
	public const int SEND_ARENA_NARROW_AREA = 110003;

	// Token: 0x040016B5 RID: 5813
	public const int SEND_ARENA_FIGHT_START = 110004;

	// Token: 0x040016B6 RID: 5814
	public const int SEND_ARENA_FIGHT_END = 110005;

	// Token: 0x040016B7 RID: 5815
	public const int SEND_ARENA_COUNTDOWN = 110006;

	// Token: 0x040016B8 RID: 5816
	public const int RECEIVE_ARENA_AREA_HURT = 110007;

	// Token: 0x040016B9 RID: 5817
	public const int SEND_ARENA_FIGHT_PREPARE = 110009;

	// Token: 0x040016BA RID: 5818
	public const int SEND_ARENA_ROUND_END = 110010;

	// Token: 0x040016BB RID: 5819
	public const int SEND_ARENA_ROUND_START = 110011;

	// Token: 0x040016BC RID: 5820
	public const int SYSTEM_ARENA_SCORE = 110300;

	// Token: 0x040016BD RID: 5821
	public const int SEND_ARENA_SCORE_CREATE = 110301;

	// Token: 0x040016BE RID: 5822
	public const int SEND_ARENA_SCORE_FIGHT_DATA = 110302;

	// Token: 0x040016BF RID: 5823
	public const int SEND_ARENA_SCORE_FIGHT_START = 110303;

	// Token: 0x040016C0 RID: 5824
	public const int SEND_ARENA_SCORE_FIGHT_END = 110304;

	// Token: 0x040016C1 RID: 5825
	public const int SEND_ARENA_SCORE_COUNTDOWN = 110305;

	// Token: 0x040016C2 RID: 5826
	public const int SEND_ARENA_SCORE_FIGHT_RESULT = 110306;

	// Token: 0x040016C3 RID: 5827
	public const int SEND_ARENA_SCORE_FIGHT_PREPARE = 110307;

	// Token: 0x040016C4 RID: 5828
	public const int SEND_ARENA_SCORE_ROUND_END = 110308;

	// Token: 0x040016C5 RID: 5829
	public const int SEND_ARENA_SCORE_ROUND_START = 110309;

	// Token: 0x040016C6 RID: 5830
	public const int SYSTEM_SCRAMBLE = 110100;

	// Token: 0x040016C7 RID: 5831
	public const int SEND_SCRAMBLE_CREATE = 110101;

	// Token: 0x040016C8 RID: 5832
	public const int SEND_SCRAMBLE_COUNTDOWN = 110102;

	// Token: 0x040016C9 RID: 5833
	public const int SEND_SCRAMBLE_USER_DATA = 110103;

	// Token: 0x040016CA RID: 5834
	public const int SEND_SCRAMBLE_FIGHT_SCORE = 110104;

	// Token: 0x040016CB RID: 5835
	public const int SEND_SCRAMBLE_FIGHT_DATA = 110105;

	// Token: 0x040016CC RID: 5836
	public const int SEND_SCRAMBLE_FIGHT_PREPARE = 110106;

	// Token: 0x040016CD RID: 5837
	public const int SEND_SCRAMBLE_FIGHT_START = 110107;

	// Token: 0x040016CE RID: 5838
	public const int SEND_SCRAMBLE_FIGHT_RESULT = 110108;

	// Token: 0x040016CF RID: 5839
	public const int SEND_SCRAMBLE_FIGHT_END = 110109;

	// Token: 0x040016D0 RID: 5840
	public const int SYSTEM_TRAINING = 110200;

	// Token: 0x040016D1 RID: 5841
	public const int SEND_TRAINING_FIGHT_START = 110201;

	// Token: 0x040016D2 RID: 5842
	public const int SUICIDE = 1000000;

	// Token: 0x040016D3 RID: 5843
	public const int HURT = 1000001;

	// Token: 0x040016D4 RID: 5844
	public const int CLT_GETQQREWARD = 3450;

	// Token: 0x040016D5 RID: 5845
	public const int CLT_SETQQREWARD = 3451;

	// Token: 0x040016D6 RID: 5846
	public const int NPC = 1010000;

	// Token: 0x040016D7 RID: 5847
	public const int NPC_CREATE = 1010001;

	// Token: 0x040016D8 RID: 5848
	public const int NPC_SYNCHRONIZATION = 1010002;

	// Token: 0x040016D9 RID: 5849
	public const int NPC_HURT = 1010003;

	// Token: 0x040016DA RID: 5850
	public const int NPC_SUICIDE = 1010004;

	// Token: 0x040016DB RID: 5851
	public const int NPC_DEATH = 1010005;

	// Token: 0x040016DC RID: 5852
	public const int NPC_ATTACK_START = 1010006;

	// Token: 0x040016DD RID: 5853
	public const int NPC_ATTACK_STOP = 1010007;

	// Token: 0x040016DE RID: 5854
	public const int NPC_TRANSMISSION = 1010100;

	// Token: 0x040016DF RID: 5855
	public const int AI_SYNCHRONIZATION = 1020000;

	// Token: 0x040016E0 RID: 5856
	public const int AI_SYNCHRONIZATION_VALIDATE = 1020001;

	// Token: 0x040016E1 RID: 5857
	public const int AI_SYNCHRONIZATION_VALIDATE_SUCCEED = 1020002;

	// Token: 0x040016E2 RID: 5858
	public const int AI_SYNCHRONIZATION_CHANGE = 1020003;

	// Token: 0x040016E3 RID: 5859
	public const int AI_SYNCHRONIZATION_VALIDATE_REQUEST_TIMEOUT = 1020004;

	// Token: 0x040016E4 RID: 5860
	public const int DOOR_HANDLE = 1030000;

	// Token: 0x040016E5 RID: 5861
	public const int REINHARDT = 2010000;

	// Token: 0x040016E6 RID: 5862
	public const int REINHARDT_SHIELD_STATE_SYNCHRONIZATION = 2010001;

	// Token: 0x040016E7 RID: 5863
	public const int REINHARDT_SHIELD_SYNCHRONIZATION = 2010002;

	// Token: 0x040016E8 RID: 5864
	public const int REINHARDT_SHIELD_BREAK_SYNCHRONIZATION = 2010003;

	// Token: 0x040016E9 RID: 5865
	public const int REINHARDT_DASH_SYNCHRONIZATION = 2010100;

	// Token: 0x040016EA RID: 5866
	public const int REINHARDT_DASH_CONFIRM_ADSORB_SYNCHRONIZATION = 2010101;

	// Token: 0x040016EB RID: 5867
	public const int REINHARDT_DASH_CANCEL_ADSORB_SYNCHRONIZATION = 2010102;

	// Token: 0x040016EC RID: 5868
	public const int REINHARDT_DASH_START_SYNCHRONIZATION = 2010103;

	// Token: 0x040016ED RID: 5869
	public const int REINHARDT_DASH_END_SYNCHRONIZATION = 2010104;

	// Token: 0x040016EE RID: 5870
	public const int REINHARDT_DASH_SLAMMED_SYNCHRONIZATION = 2010105;

	// Token: 0x040016EF RID: 5871
	public const int DAJI_SHIELD_STATE_SYNCHRONIZATION = 2100001;

	// Token: 0x040016F0 RID: 5872
	public const int DAJI_SHIELD_SYNCHRONIZATION = 2010002;

	// Token: 0x040016F1 RID: 5873
	public const int LLS_SHIELD_ENERGYDATA = 2210001;

	// Token: 0x040016F2 RID: 5874
	public const int LLS_FLYSHIELD_DATA = 2210002;

	// Token: 0x040016F3 RID: 5875
	public const int SETTING = 3010000;

	// Token: 0x040016F4 RID: 5876
	public const int SETTING_SEND_DATA = 3010001;

	// Token: 0x040016F5 RID: 5877
	public const int SETTING_LOAD_DATA = 3010011;

	// Token: 0x040016F6 RID: 5878
	public const int SETTING_LOAD_DATA_RESULT = 3010012;

	// Token: 0x040016F7 RID: 5879
	public const int SETTING_SAVE_DATA = 3010013;

	// Token: 0x040016F8 RID: 5880
	public const int SETTING_SAVE_DATA_RESULT = 3010014;

	// Token: 0x040016F9 RID: 5881
	public const int SETTING_UPDATE_DATA = 3010015;

	// Token: 0x040016FA RID: 5882
	public const int SETTING_UPDATE_DATA_RESULT = 3010016;

	// Token: 0x040016FB RID: 5883
	public const int SETTING_UPDATE_GRAPHIC_CARD_LADDER = 3010017;

	// Token: 0x040016FC RID: 5884
	public const int SETTING_STATISTIC_FPS = 3010019;

	// Token: 0x040016FD RID: 5885
	public const int SETTING_HANDLE_FPS = 3010020;

	// Token: 0x040016FE RID: 5886
	public const int GEM = 3020000;

	// Token: 0x040016FF RID: 5887
	public const int GEM_SEND_DATA = 3020001;

	// Token: 0x04001700 RID: 5888
	public const int GEM_LOAD_DATA = 3020002;

	// Token: 0x04001701 RID: 5889
	public const int GEM_LOAD_DATA_RESULT = 3020003;

	// Token: 0x04001702 RID: 5890
	public const int GEM_SAVE_DATA = 3020004;

	// Token: 0x04001703 RID: 5891
	public const int GEM_SAVE_DATA_RESULT = 3020005;

	// Token: 0x04001704 RID: 5892
	public const int GEM_UPDATE_DATA = 3020006;

	// Token: 0x04001705 RID: 5893
	public const int GEM_INTERFACE = 3020100;

	// Token: 0x04001706 RID: 5894
	public const int GEM_GET = 3020101;

	// Token: 0x04001707 RID: 5895
	public const int GEM_GET_RESULT = 3020102;

	// Token: 0x04001708 RID: 5896
	public const int GEM_DELETE = 3020103;

	// Token: 0x04001709 RID: 5897
	public const int GEM_DELETE_RESULT = 3020104;

	// Token: 0x0400170A RID: 5898
	public const int GEM_SELL = 3020105;

	// Token: 0x0400170B RID: 5899
	public const int GEM_SELL_RESULT = 3020106;

	// Token: 0x0400170C RID: 5900
	public const int GEM_EQUIP = 3020107;

	// Token: 0x0400170D RID: 5901
	public const int GEM_EQUIP_RESULT = 3020108;

	// Token: 0x0400170E RID: 5902
	public const int GEM_UNEQUIP = 3020109;

	// Token: 0x0400170F RID: 5903
	public const int GEM_UNEQUIP_RESULT = 3020110;

	// Token: 0x04001710 RID: 5904
	public const int GEM_SYNTHETIC = 3020111;

	// Token: 0x04001711 RID: 5905
	public const int GEM_SYNTHETIC_RESULT = 3020112;

	// Token: 0x04001712 RID: 5906
	public const int GEM_SYNTHETIC_PLACE = 3020113;

	// Token: 0x04001713 RID: 5907
	public const int GEM_SYNTHETIC_PLACE_RESULT = 3020114;

	// Token: 0x04001714 RID: 5908
	public const int GEM_BATCH_SYNTHETIC = 3020115;

	// Token: 0x04001715 RID: 5909
	public const int GEM_BATCH_SYNTHETIC_RESULT = 3020116;

	// Token: 0x04001716 RID: 5910
	public const int GEM_HAVE_NEW = 3020117;

	// Token: 0x04001717 RID: 5911
	public const int GEM_SYNTHETIC_PLACE_SUCCESS_RATE = 3020118;

	// Token: 0x04001718 RID: 5912
	public const int GEM_SYNTHETIC_PLACE_SUCCESS_RATE_RESULT = 3020119;

	// Token: 0x04001719 RID: 5913
	public const int PREROGATIVE = 302000;

	// Token: 0x0400171A RID: 5914
	public const int PREROGATIVE_GET_REWARD = 3020001;

	// Token: 0x0400171B RID: 5915
	public const int PREROGATIVE_GET_DATA = 302002;

	// Token: 0x0400171C RID: 5916
	public const int PREROGATIVE_GET_REWARD_DATA = 302003;

	// Token: 0x0400171D RID: 5917
	public const int SYSTEM_DETECTOR = 3030000;

	// Token: 0x0400171E RID: 5918
	public const int SYSTEM_PROCESS_DETECTOR = 3030001;

	// Token: 0x0400171F RID: 5919
	public const int GM_MSG_GETPLAYERPOS = 8201;

	// Token: 0x04001720 RID: 5920
	public const int GM_MSG_ALLPLAYERNUM = 8203;

	// Token: 0x04001721 RID: 5921
	public const int GM_MSG_SKYMATCHNUM = 8204;

	// Token: 0x04001722 RID: 5922
	public const int TRACER_GO_TO_PAST = 2020001;

	// Token: 0x04001723 RID: 5923
	public const int SHAWANG_SANDSTORM_DASH = 2030001;

	// Token: 0x04001724 RID: 5924
	public const int SHAWANG_SUPER_SKILL = 2030002;

	// Token: 0x04001725 RID: 5925
	public const int SHAWANG_SUPERPULL = 2030003;

	// Token: 0x04001726 RID: 5926
	public const int GUANYIN = 2040000;

	// Token: 0x04001727 RID: 5927
	public const int GUANYIN_REVIVE = 2040001;

	// Token: 0x04001728 RID: 5928
	public const int LUOJI_DAMAGER = 2050001;

	// Token: 0x04001729 RID: 5929
	public const int JINGKE_ADDBUFFE_SELF = 2060001;

	// Token: 0x0400172A RID: 5930
	public const int LUOLI_ADDBLOOD_HUMAN = 2070001;

	// Token: 0x0400172B RID: 5931
	public const int DAWEI_CAR_BLACK = 2080001;

	// Token: 0x0400172C RID: 5932
	public const int HP_COMMON = 1;

	// Token: 0x0400172D RID: 5933
	public const int HP_BUFF = 2;

	// Token: 0x0400172E RID: 5934
	public const int HP_SHIELD_ONLINE = 3;

	// Token: 0x0400172F RID: 5935
	public const int HP_SHIELD_JIDI = 4;

	// Token: 0x04001730 RID: 5936
	public const int HP_REVIVE = 5;

	// Token: 0x04001731 RID: 5937
	public const int HP_BUFFFUWEN = 6;

	// Token: 0x04001732 RID: 5938
	public const float UISOUNDDELAY = 3.5f;

	// Token: 0x04001733 RID: 5939
	public const int CHAT_SVR_MSG = 1000;

	// Token: 0x04001734 RID: 5940
	public const int CHAT_MSG_LOGIN = 1001;

	// Token: 0x04001735 RID: 5941
	public const int CHAT_MSG_CRETEGROUP = 1002;

	// Token: 0x04001736 RID: 5942
	public const int CHAT_MSG_JOIN_GROUP = 1003;

	// Token: 0x04001737 RID: 5943
	public const int CHAT_MSG_EXIT_GROUP = 1004;

	// Token: 0x04001738 RID: 5944
	public const int CHAT_MSG_CREATDATA = 1005;

	// Token: 0x04001739 RID: 5945
	public const int CHAT_MSG_DATA = 1006;

	// Token: 0x0400173A RID: 5946
	public const int CHAT_MSG_HUDONG = 1012;

	// Token: 0x0400173B RID: 5947
	public const int PLAYERT_MSG_REPOR = 6001;

	// Token: 0x0400173C RID: 5948
	public const int TEAM_MSG_COM = 60000;

	// Token: 0x0400173D RID: 5949
	public const int TEAM_MSG_CREATE = 60001;

	// Token: 0x0400173E RID: 5950
	public const int TEAM_MSG_JOIN = 60002;

	// Token: 0x0400173F RID: 5951
	public const int TEAM_MSG_LEAVE = 60003;

	// Token: 0x04001740 RID: 5952
	public const int TEAM_MSG_CHANGE = 60004;

	// Token: 0x04001741 RID: 5953
	public const int TEAM_MSG_INVITE = 60005;

	// Token: 0x04001742 RID: 5954
	public const int TEAM_MSG_JUJUE = 60006;

	// Token: 0x04001743 RID: 5955
	public const int TEAM_MSG_INTEAM = 60007;

	// Token: 0x04001744 RID: 5956
	public const int TEAM_MSG_RETURNMSG = 60008;

	// Token: 0x04001745 RID: 5957
	public const int TEAM_MSG_TEAMKICK = 60009;

	// Token: 0x04001746 RID: 5958
	public const int TEAM_MSG_GETMETEAM = 60010;

	// Token: 0x04001747 RID: 5959
	public const int TEAM_MSG_UPSTATE = 60011;

	// Token: 0x04001748 RID: 5960
	public const int TEAM_MSG_GETTEAMSTATE = 60012;

	// Token: 0x04001749 RID: 5961
	public const int TEAM_MSG_LOGINLEAVE = 60013;

	// Token: 0x0400174A RID: 5962
	public const int TEAM_MSG_UPDATESKYDATA = 60016;

	// Token: 0x0400174B RID: 5963
	public const int TEAM_MSG_SKYTEAMMSG = 60017;

	// Token: 0x0400174C RID: 5964
	public const int TEAM_MSG_GETLOOGINGTEAM = 60018;

	// Token: 0x0400174D RID: 5965
	public const int TEAM_MSG_CREATELOOKINGTEAM = 60019;

	// Token: 0x0400174E RID: 5966
	public const int TEAM_MSG_LEAVELOOGINGTEAM = 60020;

	// Token: 0x0400174F RID: 5967
	public const int TEAM_MSG_APPLYJOINTEAM = 60021;

	// Token: 0x04001750 RID: 5968
	public const int TEAM_MSG_UPDATELOOKTEAM = 60022;

	// Token: 0x04001751 RID: 5969
	public const int TEAM_MSG_CREATEERROR = 60024;

	// Token: 0x04001752 RID: 5970
	public const int TEAM_MSG_YAOQINGLEAVE = 60025;

	// Token: 0x04001753 RID: 5971
	public const int TEAM_MSG_CHANGENICK = 60026;

	// Token: 0x04001754 RID: 5972
	public const int FRIEND_MSG_COM = 60100;

	// Token: 0x04001755 RID: 5973
	public const int FRIEND_MSG_FIND = 60101;

	// Token: 0x04001756 RID: 5974
	public const int FRIEND_MSG_ADD = 60102;

	// Token: 0x04001757 RID: 5975
	public const int FRIEND_MSG_DEL = 60103;

	// Token: 0x04001758 RID: 5976
	public const int FRIEND_MSG_UPDATE = 60104;

	// Token: 0x04001759 RID: 5977
	public const int FRIEND_MSG_REQUST = 60105;

	// Token: 0x0400175A RID: 5978
	public const int FRIEND_MSG_REMSG = 60106;

	// Token: 0x0400175B RID: 5979
	public const int FRIEND_MSG_USERINFO = 60107;

	// Token: 0x0400175C RID: 5980
	public const int FRIEND_MSG_ADDUSER = 60109;

	// Token: 0x0400175D RID: 5981
	public const int FRIEND_MSG_FINDAPPLYUSER = 60114;

	// Token: 0x0400175E RID: 5982
	public const int FRIEND_MSG_ADDUNMSG = 60115;

	// Token: 0x0400175F RID: 5983
	public const int FRIEND_MSG_LOADFRIENDBASE = 60118;

	// Token: 0x04001760 RID: 5984
	public const int FRIEND_MSG_UPDATEFRIENDLATELY = 60200;

	// Token: 0x04001761 RID: 5985
	public const int FRIEND_MSG_SKYLEVEL = 60204;

	// Token: 0x04001762 RID: 5986
	public const int HUDONG_SVR_MSG = 2000;

	// Token: 0x04001763 RID: 5987
	public const int HUDONG_SVR_MATCHINFO = 2001;

	// Token: 0x04001764 RID: 5988
	public const int HUDONG_SVR_UIMSG = 2002;

	// Token: 0x04001765 RID: 5989
	public const int SVR_RADIO = 3000;

	// Token: 0x04001766 RID: 5990
	public const int SVR_RADIO_SCN = 3001;

	// Token: 0x04001767 RID: 5991
	public const int SVR_UPGONGGAO = 3002;

	// Token: 0x04001768 RID: 5992
	public const int SVR_UPGONGGAO_READ = 3003;

	// Token: 0x04001769 RID: 5993
	public const int SVR_CLOSESERVER_READ = 3004;

	// Token: 0x0400176A RID: 5994
	public const int SHOP_MSG_DATA = 70000;

	// Token: 0x0400176B RID: 5995
	public const int SHOP_MSG_BUYITEM = 70001;

	// Token: 0x0400176C RID: 5996
	public const int SHOP_MSG_GETLIMIT = 70002;

	// Token: 0x0400176D RID: 5997
	public const int SHOP_MSG_BUYLIMITITEM = 70003;

	// Token: 0x0400176E RID: 5998
	public const int SHOP_MSG_UPDATELIST = 70004;

	// Token: 0x0400176F RID: 5999
	public const int SHOP_MSG_UPDATELIBAO = 70005;

	// Token: 0x04001770 RID: 6000
	public const int SHOP_MSG_OPENLIBAO = 70006;

	// Token: 0x04001771 RID: 6001
	public const int SHOP_MSG_ITEMBUFF = 70007;

	// Token: 0x04001772 RID: 6002
	public const int SHOP_MSG_ITEMHUIGOU = 70008;

	// Token: 0x04001773 RID: 6003
	public const int SHOP_MSG_ADDITEM = 70009;

	// Token: 0x04001774 RID: 6004
	public const int SHOP_MSG_OPENALLLIBAO = 70010;

	// Token: 0x04001775 RID: 6005
	public const int SHOP_MSG_ZENGSONG = 70011;

	// Token: 0x04001776 RID: 6006
	public const int SHOP_MSG_ZSADDITEM = 70012;

	// Token: 0x04001777 RID: 6007
	public const int SHOP_GIVE_CHECKUSER = 70013;

	// Token: 0x04001778 RID: 6008
	public const int SHOP_GIVE_SAVEDATA = 70014;

	// Token: 0x04001779 RID: 6009
	public const int SHOP_GIVE_LOADDATA = 70015;

	// Token: 0x0400177A RID: 6010
	public const int SHOP_GIVE_FINDUSER = 70016;

	// Token: 0x0400177B RID: 6011
	public const int SHOP_MSG_UPDATEITEM = 70018;

	// Token: 0x0400177C RID: 6012
	public const int SHOP_MSG_EXCHANGEITEM = 70019;

	// Token: 0x0400177D RID: 6013
	public const int MATCH_MSG_COM = 80001;

	// Token: 0x0400177E RID: 6014
	public const int MATCH_MSG_STAR = 80002;

	// Token: 0x0400177F RID: 6015
	public const int MATCH_MSG_ESC = 80003;

	// Token: 0x04001780 RID: 6016
	public const int MATCH_MSG_CREATEFB = 80004;

	// Token: 0x04001781 RID: 6017
	public const int MATCH_MSG_HERO = 80005;

	// Token: 0x04001782 RID: 6018
	public const int MATCH_MSG_MSGSTAR = 80006;

	// Token: 0x04001783 RID: 6019
	public const int MATCH_MSG_MSGESC = 80007;

	// Token: 0x04001784 RID: 6020
	public const int MATCH_MSG_UPDATANUM = 80011;

	// Token: 0x04001785 RID: 6021
	public const int MATCH_MSG_GETMATCHNUM = 80012;

	// Token: 0x04001786 RID: 6022
	public const int MATCH_MSG_MATCHOK = 80013;

	// Token: 0x04001787 RID: 6023
	public const int MATCH_MSG_SETMATCHTYPE = 80016;

	// Token: 0x04001788 RID: 6024
	public const int MATCH_MSG_CHECKTIP = 80021;

	// Token: 0x04001789 RID: 6025
	public const int SVR_REV_MATCHFB = 20014;

	// Token: 0x0400178A RID: 6026
	public const int FB_MATCH_MSG = 90001;

	// Token: 0x0400178B RID: 6027
	public const int FB_MATCH_SHERO = 90002;

	// Token: 0x0400178C RID: 6028
	public const int FB_MATCH_AHERO = 90003;

	// Token: 0x0400178D RID: 6029
	public const int FB_MATCH_DOWNHERO = 90004;

	// Token: 0x0400178E RID: 6030
	public const int FB_MATCH_ISCHANGER = 90005;

	// Token: 0x0400178F RID: 6031
	public const int FB_MATCH_ALLUPDATA = 90006;

	// Token: 0x04001790 RID: 6032
	public const int FB_MATCH_DELUSER = 90007;

	// Token: 0x04001791 RID: 6033
	public const int FB_MATCH_CHECKHERO = 90008;

	// Token: 0x04001792 RID: 6034
	public const int FB_MATCH_GMENTER = 90009;

	// Token: 0x04001793 RID: 6035
	public const int CAREER_GETDATA = 100001;

	// Token: 0x04001794 RID: 6036
	public const string YOUMEKEY = "YOUME8661B5FFE856F6887D41B94F6CC66F070A56BF09";

	// Token: 0x04001795 RID: 6037
	public const string YOUMESERVER = "n96jbfQ/ibca2M8qRZV1+BByDmvG7wB57/hD/a6J/wW24hovSL887ySfODNhxBTYa7ZCxVKGG6Cjdo98TtVnHvb2KzNjbCg2/U2o7eCxdgft5ihIYsbsMMCb+AKC89DBjVGD7iDG8ZW2rZmz7DcR4Y3C5jJlLhFLa6Z2vguRArcBAAE=";

	// Token: 0x04001796 RID: 6038
	public Dictionary<int, float> m_szDongJieEffSize = new Dictionary<int, float>
	{
		{
			1003,
			1f
		},
		{
			1005,
			1f
		},
		{
			1015,
			1f
		},
		{
			1016,
			1f
		},
		{
			1017,
			1f
		},
		{
			1019,
			1f
		},
		{
			1021,
			1f
		},
		{
			1022,
			1f
		},
		{
			1023,
			1f
		},
		{
			1024,
			1f
		},
		{
			1025,
			1f
		}
	};

	// Token: 0x04001797 RID: 6039
	public const int PLAYER_MSG_GETFACE = 11001;

	// Token: 0x04001798 RID: 6040
	public const int PLAYER_MSG_OPENFACE = 11002;

	// Token: 0x04001799 RID: 6041
	public const int PLAYER_MSG_SETFACE = 11003;

	// Token: 0x0400179A RID: 6042
	public const int PLAYER_MSG_CHANGENEW = 11004;

	// Token: 0x0400179B RID: 6043
	public const int PLAYER_MSG_GETOLDREWARD = 11006;

	// Token: 0x0400179C RID: 6044
	public const int PLAYER_MSG_GETPAINT = 11008;

	// Token: 0x0400179D RID: 6045
	public const int PLAYER_MSG_OPENPAINT = 11009;

	// Token: 0x0400179E RID: 6046
	public const int PLAYER_MSG_SETPAINT = 11010;

	// Token: 0x0400179F RID: 6047
	public const int PLAYER_MSG_GIFTPAINT = 11011;

	// Token: 0x040017A0 RID: 6048
	public const int PLAYER_MSG_GIFTPAINTBACK = 11012;

	// Token: 0x040017A1 RID: 6049
	public const int PLAYER_MSG_GIFTFACE = 11013;

	// Token: 0x040017A2 RID: 6050
	public const int PLAYER_MSG_GIFTFACEBACK = 11014;

	// Token: 0x040017A3 RID: 6051
	public const int HERO_MSG_OPEN = 12001;

	// Token: 0x040017A4 RID: 6052
	public const int HERO_MSG_GETMYDATA = 12002;

	// Token: 0x040017A5 RID: 6053
	public const int HERO_MSG_LVLUP = 12005;

	// Token: 0x040017A6 RID: 6054
	public const int HERO_MSG_UPDATEONE = 12006;

	// Token: 0x040017A7 RID: 6055
	public const int HERO_MSG_CHIPOPEN = 12007;

	// Token: 0x040017A8 RID: 6056
	public const int HERO_MSG_UNLOCKOPEN = 12008;

	// Token: 0x040017A9 RID: 6057
	public const int HERO_MSG_CHANGEUSE = 12009;

	// Token: 0x040017AA RID: 6058
	public const int HERO_MSG_SENDAWARD = 12010;

	// Token: 0x040017AB RID: 6059
	public const int HERO_MSG_EQUIPCHIP = 12011;

	// Token: 0x040017AC RID: 6060
	public const int HERO_MSG_MATCHDATA = 12012;

	// Token: 0x040017AD RID: 6061
	public const int HERO_MSG_EQUIPNEWSTATE = 12013;

	// Token: 0x040017AE RID: 6062
	public const int HERO_MSG_NEWSTATE = 12014;

	// Token: 0x040017AF RID: 6063
	public const int HERO_MSG_OPENUITIME = 12015;

	// Token: 0x040017B0 RID: 6064
	public const int HERO_MSG_GIFTOPEN = 12016;

	// Token: 0x040017B1 RID: 6065
	public const int HERO_MSG_GIFTOPENBACK = 12017;

	// Token: 0x040017B2 RID: 6066
	public const int HERO_MSG_UNLOCKEND = 12018;

	// Token: 0x040017B3 RID: 6067
	public const int HERO_MSG_GRIDOPEN = 12019;

	// Token: 0x040017B4 RID: 6068
	public const int HERO_MSG_WEAPPARTS = 12020;

	// Token: 0x040017B5 RID: 6069
	public const int HERO_MSG_WEAPPARTSLVLUP = 12021;

	// Token: 0x040017B6 RID: 6070
	public const int HERO_MSG_WEAPPARTSUSE = 12022;

	// Token: 0x040017B7 RID: 6071
	public const int HERO_MSG_PARTSREMOVE = 12023;

	// Token: 0x040017B8 RID: 6072
	public const int HERO_MSG_WEAPPARTSOPEN = 12024;

	// Token: 0x040017B9 RID: 6073
	public const int CLT_MSG_ISTEMPCHECK = 0;

	// Token: 0x040017BA RID: 6074
	public const int FCM_MSG_FCMSTATE = 13001;

	// Token: 0x040017BB RID: 6075
	public const int FCM_MSG_ONLINE = 13002;

	// Token: 0x040017BC RID: 6076
	public const int FCM_MSG_OUTLINE = 13003;

	// Token: 0x040017BD RID: 6077
	public const int FCM_MSG_SHOWTIP = 13004;

	// Token: 0x040017BE RID: 6078
	public const int FCM_MSG_FCMDATA = 13005;

	// Token: 0x040017BF RID: 6079
	public const int FCM_MSG_CERTIFICATE = 13006;

	// Token: 0x040017C0 RID: 6080
	public const int CLT_MSG_ZHONGTUJIARU = 20;

	// Token: 0x040017C1 RID: 6081
	public const int TASK_MSG_DATA = 71000;

	// Token: 0x040017C2 RID: 6082
	public const int TASK_MSG_INITDATA = 71001;

	// Token: 0x040017C3 RID: 6083
	public const int TASK_MSG_UPDATEDATA = 71002;

	// Token: 0x040017C4 RID: 6084
	public const int TASK_MSG_UPDATEHIS = 71003;

	// Token: 0x040017C5 RID: 6085
	public const int TASK_MSG_ACC = 71004;

	// Token: 0x040017C6 RID: 6086
	public const int TASK_MSG_COM = 71005;

	// Token: 0x040017C7 RID: 6087
	public const int TASK_MSG_REFPINZHI = 71006;

	// Token: 0x040017C8 RID: 6088
	public const int TASK_MSG_REWARD = 71007;

	// Token: 0x040017C9 RID: 6089
	public const int TASK_MSG_TISHI = 71008;

	// Token: 0x040017CA RID: 6090
	public const int TASK_MSG_UPDATECHAPTERDATA = 71009;

	// Token: 0x040017CB RID: 6091
	public const int GUILD_MSG_DATA = 91000;

	// Token: 0x040017CC RID: 6092
	public const int GUILD_MSG_CREATEGUILD = 91001;

	// Token: 0x040017CD RID: 6093
	public const int GUILD_MSG_DISGUILD = 91002;

	// Token: 0x040017CE RID: 6094
	public const int GUILD_MSG_ADDGUILD = 91003;

	// Token: 0x040017CF RID: 6095
	public const int GUILD_MSG_EXITGUILD = 91004;

	// Token: 0x040017D0 RID: 6096
	public const int GUILD_MSG_KICKUSER = 91005;

	// Token: 0x040017D1 RID: 6097
	public const int GUILD_MSG_UPGRADE = 91006;

	// Token: 0x040017D2 RID: 6098
	public const int GUILD_MSG_DEMISE = 91007;

	// Token: 0x040017D3 RID: 6099
	public const int GUILD_MSG_SETRANK = 91008;

	// Token: 0x040017D4 RID: 6100
	public const int GUILD_MSG_SETTILE = 91009;

	// Token: 0x040017D5 RID: 6101
	public const int GUILD_MSG_LOGIN = 91010;

	// Token: 0x040017D6 RID: 6102
	public const int GUILD_MSG_LOGOFF = 91011;

	// Token: 0x040017D7 RID: 6103
	public const int GUILD_MSG_EXIST = 91012;

	// Token: 0x040017D8 RID: 6104
	public const int GUILD_MSG_SELECTALL = 91013;

	// Token: 0x040017D9 RID: 6105
	public const int GUILD_MSG_GETGUILDLVL = 91014;

	// Token: 0x040017DA RID: 6106
	public const int GUILD_MSG_ACCEPTREQUEST = 91015;

	// Token: 0x040017DB RID: 6107
	public const int GUILD_MSG_RFREQUEST = 91016;

	// Token: 0x040017DC RID: 6108
	public const int GUILD_MSG_GETREQLIST = 91017;

	// Token: 0x040017DD RID: 6109
	public const int GUILD_MSG_GETGUILDDATA = 91018;

	// Token: 0x040017DE RID: 6110
	public const int GUILD_MSG_SAVENOTICE = 91019;

	// Token: 0x040017DF RID: 6111
	public const int GUILD_MSG_USERGUILDINFO = 91020;

	// Token: 0x040017E0 RID: 6112
	public const int GUILD_MSG_DELJOIN = 91021;

	// Token: 0x040017E1 RID: 6113
	public const int GUILD_MSG_CHGAUTOJOIN = 91022;

	// Token: 0x040017E2 RID: 6114
	public const int GUILD_MSG_INVITE = 91023;

	// Token: 0x040017E3 RID: 6115
	public const int GUILD_MSG_BEINVITE = 91024;

	// Token: 0x040017E4 RID: 6116
	public const int GUILD_MSG_ADDACTIVE = 91025;

	// Token: 0x040017E5 RID: 6117
	public const int GUILD_MSG_BEINVITEOK = 91026;

	// Token: 0x040017E6 RID: 6118
	public const int GUILD_MSG_UPDATEREQNUM = 91027;

	// Token: 0x040017E7 RID: 6119
	public const int GUILD_MSG_HAVEGUILD = 91028;

	// Token: 0x040017E8 RID: 6120
	public const int GUILD_MSG_FINDER = 91031;

	// Token: 0x040017E9 RID: 6121
	public const int GUILD_MSG_GUILDNAME = 91033;

	// Token: 0x040017EA RID: 6122
	public const int GUILD_MSG_TANHE = 91034;

	// Token: 0x040017EB RID: 6123
	public const int GUILD_MSG_CHECKTANHE = 91035;

	// Token: 0x040017EC RID: 6124
	public const int GUILD_MSG_FULIDATA = 91036;

	// Token: 0x040017ED RID: 6125
	public const int GUILD_MSG_GETFULI = 91037;

	// Token: 0x040017EE RID: 6126
	public const int ISMATCH = 1;

	// Token: 0x040017EF RID: 6127
	public const int LEARNSCNID = 5205;

	// Token: 0x040017F0 RID: 6128
	public const int CREATE_ROBOTAI = 4010000;

	// Token: 0x040017F1 RID: 6129
	public const int ROBOTAI_CHECKPLAYER = 4010001;

	// Token: 0x040017F2 RID: 6130
	public const int ROBOTAI_TOG_SELECT_SEARCH = 4010002;

	// Token: 0x040017F3 RID: 6131
	public const int ROBOTAI_CHECKPLAYER_OK = 4010003;

	// Token: 0x040017F4 RID: 6132
	public const int ROBOTAI_CHECKPLAYER_CANCEL = 4010004;

	// Token: 0x040017F5 RID: 6133
	public const int WENJUAN_DATA = 14001;

	// Token: 0x040017F6 RID: 6134
	public const int WENJUAN_LOADDATA = 14002;

	// Token: 0x040017F7 RID: 6135
	public const int WENJUAN_REWARDDATA = 14003;

	// Token: 0x040017F8 RID: 6136
	public const int WENJUAN_LOADCHECKDATA = 14004;

	// Token: 0x040017F9 RID: 6137
	public const int WENJUAN_UPDATA = 14005;

	// Token: 0x040017FA RID: 6138
	public const int WENJUAN_OPENUILOG = 14006;

	// Token: 0x040017FB RID: 6139
	public const int LIBAO_DATA = 15001;

	// Token: 0x040017FC RID: 6140
	public const int LIBAO_UPDATESHOUCHONG = 15002;

	// Token: 0x040017FD RID: 6141
	public const int LIBAO_REWARDSHOUCHONG = 15003;

	// Token: 0x040017FE RID: 6142
	public const int LIBAO_UPDATEDENGLU = 15004;

	// Token: 0x040017FF RID: 6143
	public const int LIBAO_REWARDDENGLU = 15005;

	// Token: 0x04001800 RID: 6144
	public const int LIBAO_LVREWARD = 15006;

	// Token: 0x04001801 RID: 6145
	public const int LIBAO_UPDATELVREWARD = 15007;

	// Token: 0x04001802 RID: 6146
	public const int LIBAO_USERLVREWARD = 15008;

	// Token: 0x04001803 RID: 6147
	public const int LIBAO_UPDATEUSERLVREWARD = 15009;

	// Token: 0x04001804 RID: 6148
	public const int LIBAO_SIGN = 15010;

	// Token: 0x04001805 RID: 6149
	public const int LIBAO_SIGN_REWARD = 15011;

	// Token: 0x04001806 RID: 6150
	public const int LIBAO_UPDATASIGN = 15012;

	// Token: 0x04001807 RID: 6151
	public const int LIBAO_UPDATASIGN_REWARD = 15013;

	// Token: 0x04001808 RID: 6152
	public const int LIBAO_ONLINEREWARD = 15014;

	// Token: 0x04001809 RID: 6153
	public const int LIBAO_UPDATEONLINE = 15015;

	// Token: 0x0400180A RID: 6154
	public const int LIBAO_ONTIMEREWARD = 15016;

	// Token: 0x0400180B RID: 6155
	public const int LIBAO_UPDATEONTIME = 15017;

	// Token: 0x0400180C RID: 6156
	public const int LIBAO_SKYACTREWARD = 15018;

	// Token: 0x0400180D RID: 6157
	public const int LIBAO_UPDATESKYACT = 15019;

	// Token: 0x0400180E RID: 6158
	public const int LIBAO_WEEKLOGIN = 15020;

	// Token: 0x0400180F RID: 6159
	public const int LIBAO_SKYEVERYDAY = 15021;

	// Token: 0x04001810 RID: 6160
	public const int LIBAO_NEWSOLDIER = 15022;

	// Token: 0x04001811 RID: 6161
	public const int LIBAO_WEEKENDONLINE = 15023;

	// Token: 0x04001812 RID: 6162
	public const int LIBAO_KAIXUEGIFT = 15024;

	// Token: 0x04001813 RID: 6163
	public const int LIBAO_MATCHREWARD = 15025;

	// Token: 0x04001814 RID: 6164
	public const int LIBAO_GODDESSGIFT = 15026;

	// Token: 0x04001815 RID: 6165
	public const int LIBAO_WEEKCHARGE = 15027;

	// Token: 0x04001816 RID: 6166
	public const int LIBAO_LUCKYDRAW = 15028;

	// Token: 0x04001817 RID: 6167
	public const int LIBAO_ONLUCKYDRAW = 15029;

	// Token: 0x04001818 RID: 6168
	public const int LIBAO_BOGOACT = 15030;

	// Token: 0x04001819 RID: 6169
	public const int LIBAO_RECHARGEZUAN = 15031;

	// Token: 0x0400181A RID: 6170
	public const int LIBAO_UPDATENADENGLU = 15032;

	// Token: 0x0400181B RID: 6171
	public const int LIBAO_RECHARGEITEM = 15033;

	// Token: 0x0400181C RID: 6172
	public const int LIBAO_NAMATCHREWARD = 15034;

	// Token: 0x0400181D RID: 6173
	public const int LIBAO_DISCOUNTITEM = 15035;

	// Token: 0x0400181E RID: 6174
	public const int LIBAO_HUILIUREWARD = 15036;

	// Token: 0x0400181F RID: 6175
	public const int LIBAO_REPAIRSIGN = 15037;

	// Token: 0x04001820 RID: 6176
	public const int LIBAO_WISHTREETIP = 15039;

	// Token: 0x04001821 RID: 6177
	public const int LIBAO_PVPWEEKREWARD = 15040;

	// Token: 0x04001822 RID: 6178
	public const int LIBAO_MULTIBOGO = 15041;

	// Token: 0x04001823 RID: 6179
	public const int LIBAO_WINTERACT = 15042;

	// Token: 0x04001824 RID: 6180
	public const int LIBAO_JIERIONLINE = 15043;

	// Token: 0x04001825 RID: 6181
	public const int USERMSG_TEXT = 16001;

	// Token: 0x04001826 RID: 6182
	public const int ACT_MSG_DATA = 16100;

	// Token: 0x04001827 RID: 6183
	public const int ACT_MSG_CHANGE = 16101;

	// Token: 0x04001828 RID: 6184
	public const int ACT_MSG_JOINDATA = 16103;

	// Token: 0x04001829 RID: 6185
	public const int ACT_MSG_JOINTEST = 16104;

	// Token: 0x0400182A RID: 6186
	public const int ACT_MSG_GETACTDATA = 16105;

	// Token: 0x0400182B RID: 6187
	public const int ACT_MSG_SEVENTARGETDATA = 16106;

	// Token: 0x0400182C RID: 6188
	public const int ACT_MSG_SEVENTARGREWARD = 16107;

	// Token: 0x0400182D RID: 6189
	public const int ACT_MSG_GETLUCKYDRAWRANK = 16108;

	// Token: 0x0400182E RID: 6190
	public const int ACT_MSG_SETLUCKYDRAWRANK = 16109;

	// Token: 0x0400182F RID: 6191
	public const int ACT_MSG_NA_REWARDDENGLU = 16110;

	// Token: 0x04001830 RID: 6192
	public const int SKY_MSG_DATA = 16200;

	// Token: 0x04001831 RID: 6193
	public const int SKY_MSG_CHANGEACTSTATE = 16201;

	// Token: 0x04001832 RID: 6194
	public const int SKY_MSG_REWARDINFO = 16202;

	// Token: 0x04001833 RID: 6195
	public const int SKY_MSG_GETREWARD = 16203;

	// Token: 0x04001834 RID: 6196
	public const int SKY_MSG_FORCEEXIT = 16204;

	// Token: 0x04001835 RID: 6197
	public const int SKY_MSG_SEASONLIST = 16205;

	// Token: 0x04001836 RID: 6198
	public const int SKY_MSG_RELINK = 16208;

	// Token: 0x04001837 RID: 6199
	public const int SKY_MSG_OPENTIP = 16209;

	// Token: 0x04001838 RID: 6200
	public const int SKY_MSG_SCORETIP = 16210;

	// Token: 0x04001839 RID: 6201
	public const int RANK_MSG_DATA = 17000;

	// Token: 0x0400183A RID: 6202
	public const int RANK_MSG_USERRANK = 17001;

	// Token: 0x0400183B RID: 6203
	public const int RANK_MSG_HERORANK = 17002;

	// Token: 0x0400183C RID: 6204
	public const int RANK_MSG_SKYLADDER = 17003;

	// Token: 0x0400183D RID: 6205
	public const int RANK_MSG_PVERANK = 17005;

	// Token: 0x0400183E RID: 6206
	public const int RANK_MSG_DTSRANK = 17006;

	// Token: 0x0400183F RID: 6207
	public const int RANK_MSG_FLOWERRANK = 17007;

	// Token: 0x04001840 RID: 6208
	public const int MAIL_AllMAILDATA = 3400;

	// Token: 0x04001841 RID: 6209
	public const int MAIL_SELMAILDATA = 3401;

	// Token: 0x04001842 RID: 6210
	public const int MAIL_CANADDONEMAIL = 3402;

	// Token: 0x04001843 RID: 6211
	public const int MAIL_READONEMAIL = 3403;

	// Token: 0x04001844 RID: 6212
	public const int MAIL_ADDMAILBACK = 3404;

	// Token: 0x04001845 RID: 6213
	public const int MAIL_DELREADMAIL = 3405;

	// Token: 0x04001846 RID: 6214
	public const int MAIL_GETMAILDRAWS = 3406;

	// Token: 0x04001847 RID: 6215
	public const int MAIL_GETUNREAD = 3407;

	// Token: 0x04001848 RID: 6216
	public const int MAIL_GETMAILSTATELIST = 3408;

	// Token: 0x04001849 RID: 6217
	public const int MAIL_DELALLREAD = 3409;

	// Token: 0x0400184A RID: 6218
	public const int MAIL_GETALLITEM = 3410;

	// Token: 0x0400184B RID: 6219
	public const int CDKEY_MSG_CREATE = 18001;

	// Token: 0x0400184C RID: 6220
	public const int CDKEY_MSG_USE = 18002;

	// Token: 0x0400184D RID: 6221
	public const int KING_MSG_DATA = 19000;

	// Token: 0x0400184E RID: 6222
	public const int KING_MSG_GAMESTART = 19001;

	// Token: 0x0400184F RID: 6223
	public const int KING_MSG_GAMEEND = 19002;

	// Token: 0x04001850 RID: 6224
	public const int KING_MSG_ROUNDSTART = 19003;

	// Token: 0x04001851 RID: 6225
	public const int KING_MSG_ROUNDEND = 19004;

	// Token: 0x04001852 RID: 6226
	public const int KING_MSG_ROUNDREADY = 19005;

	// Token: 0x04001853 RID: 6227
	public const int KING_MSG_USERINFO = 19006;

	// Token: 0x04001854 RID: 6228
	public const int KING_MSG_UPDATEUSER = 19007;

	// Token: 0x04001855 RID: 6229
	public const int KING_MSG_UPDATEMONSTER = 19008;

	// Token: 0x04001856 RID: 6230
	public const int KING_MSG_ENTERFB = 19009;

	// Token: 0x04001857 RID: 6231
	public const int KING_MSG_REVIVE = 19010;

	// Token: 0x04001858 RID: 6232
	public const int KING_MSG_LEAVE = 19011;

	// Token: 0x04001859 RID: 6233
	public const int KING_MSG_ADDUSER = 19013;

	// Token: 0x0400185A RID: 6234
	public const int KING_MSG_ALLDEADEND = 19014;

	// Token: 0x0400185B RID: 6235
	public const int HEIAN_MSG_DATA = 19100;

	// Token: 0x0400185C RID: 6236
	public const int HEIAN_MSG_ADDBUFF = 19101;

	// Token: 0x0400185D RID: 6237
	public const int PVE_MSG_DATA = 19500;

	// Token: 0x0400185E RID: 6238
	public const int PVE_MSG_UPDATE = 19501;

	// Token: 0x0400185F RID: 6239
	public const int PVE_MSG_QIANGHUA = 19502;

	// Token: 0x04001860 RID: 6240
	public const int PVE_MSG_CHECKREVIVE = 19503;

	// Token: 0x04001861 RID: 6241
	public const int PVE_MSG_JINJIE = 19504;

	// Token: 0x04001862 RID: 6242
	public const int PVE_MSG_ALLQIANGHUA = 19505;

	// Token: 0x04001863 RID: 6243
	public const int PVE_MSG_DROPLISET = 19506;

	// Token: 0x04001864 RID: 6244
	public const int PVE_MSG_FANPAI = 19507;

	// Token: 0x04001865 RID: 6245
	public const int PVE_MSG_HALLOK = 19508;

	// Token: 0x04001866 RID: 6246
	public const int PVE_MSG_AUTOFANPAI = 19509;

	// Token: 0x04001867 RID: 6247
	public const int PVE_MSG_ENERGRY = 19511;

	// Token: 0x04001868 RID: 6248
	public const int PVE_MSG_MATCH = 19512;

	// Token: 0x04001869 RID: 6249
	public const int PVE_MSG_BUYTILI = 19513;

	// Token: 0x0400186A RID: 6250
	public const int PVE_MSG_SAODANG = 19514;

	// Token: 0x0400186B RID: 6251
	public const int PVE_MSG_UPDATEOPENLV = 19515;

	// Token: 0x0400186C RID: 6252
	public const int PVE_MSG_UPDATEHA = 19516;

	// Token: 0x0400186D RID: 6253
	public const int PVE_MSG_GENEUPDATE = 19517;

	// Token: 0x0400186E RID: 6254
	public const int PVE_MSG_GENEDATA = 19518;

	// Token: 0x0400186F RID: 6255
	public const int PVE_MSG_BUYGENECHIP = 19519;

	// Token: 0x04001870 RID: 6256
	public const int PVE_MSG_FIRSTREWARD = 19520;

	// Token: 0x04001871 RID: 6257
	public const int PVE_MSG_HELPNEW = 19521;

	// Token: 0x04001872 RID: 6258
	public const int DTS_MSG_DATA = 19600;

	// Token: 0x04001873 RID: 6259
	public const int DTS_MSG_ENTERDTSDOOR = 19601;

	// Token: 0x04001874 RID: 6260
	public const int DTS_MSG_SNCMSG = 19602;

	// Token: 0x04001875 RID: 6261
	public const int DTS_MSG_DTSDOOR = 19003;

	// Token: 0x04001876 RID: 6262
	public const int DTS_MSG_GAMESTAT = 19004;

	// Token: 0x04001877 RID: 6263
	public const int DTS_MSG_GAMEDATA = 19005;

	// Token: 0x04001878 RID: 6264
	public const int DTS_MSG_SCNITEM = 19006;

	// Token: 0x04001879 RID: 6265
	public const int DTS_MSG_UPDATEUSERDATA = 19007;

	// Token: 0x0400187A RID: 6266
	public const int DTS_MSG_USEHPBOX = 19008;

	// Token: 0x0400187B RID: 6267
	public const int DTS_MSG_STARTRESULT = 19009;

	// Token: 0x0400187C RID: 6268
	public const int DTS_MSG_UPDATEQUANSTAT = 19010;

	// Token: 0x0400187D RID: 6269
	public const int DTS_MSG_PICKMSG = 19011;

	// Token: 0x0400187E RID: 6270
	public const int DTS_MSG_CHECKTIP = 19012;

	// Token: 0x0400187F RID: 6271
	public const int DTS_MSG_PERSON_FIGHT = 19700;

	// Token: 0x04001880 RID: 6272
	public const int DTS_MSG_PERSON_FIGHT_BEGIN = 19701;

	// Token: 0x04001881 RID: 6273
	public const int DTS_MSG_DTS_MSG_FUHUO = 19702;

	// Token: 0x04001882 RID: 6274
	public const int DTS_MSG_PERSON_FIGHT_END = 19703;

	// Token: 0x020005EA RID: 1514
	public enum UITRANSFROM
	{
		// Token: 0x04002E93 RID: 11923
		LobbyUI,
		// Token: 0x04002E94 RID: 11924
		GameUI,
		// Token: 0x04002E95 RID: 11925
		GameLoadUI,
		// Token: 0x04002E96 RID: 11926
		PublicUI,
		// Token: 0x04002E97 RID: 11927
		LoginUI
	}

	// Token: 0x020005EB RID: 1515
	public enum EFFECTNAME
	{
		// Token: 0x04002E99 RID: 11929
		NULL,
		// Token: 0x04002E9A RID: 11930
		JiaXue,
		// Token: 0x04002E9B RID: 11931
		JiaGong,
		// Token: 0x04002E9C RID: 11932
		NengLiangDun,
		// Token: 0x04002E9D RID: 11933
		ZhongDu,
		// Token: 0x04002E9E RID: 11934
		JiaSu,
		// Token: 0x04002E9F RID: 11935
		DongJie,
		// Token: 0x04002EA0 RID: 11936
		JianSu,
		// Token: 0x04002EA1 RID: 11937
		DianRan,
		// Token: 0x04002EA2 RID: 11938
		DaoBiao,
		// Token: 0x04002EA3 RID: 11939
		HunMi,
		// Token: 0x04002EA4 RID: 11940
		YuHe
	}

	// Token: 0x020005EC RID: 1516
	public enum PlayerState
	{
		// Token: 0x04002EA6 RID: 11942
		xuli = 1,
		// Token: 0x04002EA7 RID: 11943
		fashe,
		// Token: 0x04002EA8 RID: 11944
		daiji
	}

	// Token: 0x020005ED RID: 1517
	public enum JvJiShouState
	{
		// Token: 0x04002EAA RID: 11946
		KaiJing = 1,
		// Token: 0x04002EAB RID: 11947
		BuKaiJing
	}
}
