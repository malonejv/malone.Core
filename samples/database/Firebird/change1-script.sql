﻿ALTER TABLE "TODOLISTS" ADD "User_Id" INT
;

ALTER TABLE "TASKITEMS" ADD "Done" SMALLINT DEFAULT 0 NOT NULL CHECK("Done" IN (0,1))
;

CREATE INDEX "IX_TODOLISTS_User_Id" ON "TODOLISTS" computed by ("User_Id")
;

ALTER TABLE "TODOLISTS" ADD CONSTRAINT "FK_TODOLISTS_Users_User_Id" FOREIGN KEY ("User_Id") REFERENCES "Users" ("Id")
;

INSERT INTO "__MigrationHistory"("MigrationId", "ContextKey", "Model", "ProductVersion")
VALUES (CAST(_UTF8'202010272157481_change1' AS VARCHAR(150)), CAST(_UTF8'malone.Core.Sample.EF.Firebird.Middle.DAL.Migrations.Configuration' AS VARCHAR(300)), x'1F8B0800000000000400ED5D5B6FE3B6127E2FD0FF20E8F120B5932C4ED106768BD4897B8CE686B5B7386F012D318EB0BAB812952628FACBCEC3F949FD0B25455D78952859969D625F16B1480E87C38FE4CC7086FBD7FFFE3FF9F135F0AD1718275E144EEDB3D1A96DC1D0895C2FDC4CED143D7DF39DFDE30F5F7F35B9768357EBD7A2DE07520FB70C93A9FD8CD0F6623C4E9C6718806414784E1C25D1131A395130066E343E3F3DFD7E7C76368698848D6959D6E4631A222F80D90FFC7316850EDCA214F8B7910BFD24FF8E4B961955EB0E0430D902074EED00F8510847B32886A32508B63E1C5DCF47732F866B2F7647B79EEBE24F579737B84A88E02BB2AD4BDF0398D125F49F6C0B84618400C2C3B8F894C0258AA370B3DCE20FC05FBD6D21AEF704FC04E6C3BBA8AA9B8EF4F49C8C745C352C48396982A2A025C1B30FB9E8C662F34E136097A2C5C2BDC69380DEC8A833014F6D22D48F918F072F767631F363529197FFC285198911A5348F3195DFA3F833FDEDC16454503CB14CDA9D94083B1F9D8DBE1F9D9E58B3D447690CA7214C510CFC13EB215DFB9EF30B7C5B459F61380D53DF6707858785CBB80FF8D3431C6D618CDE3EC2A77CA80BD7B6C67CBBB1D8B06CC6B4A1425884E8C3B96DDDE1CEC1DA87256618812D111EE9CF30843140D07D0008C1382434E8D0A5DE85BEC8BF456F18A47839DAD62D78BD81E1063D4FEDF37F7F6B5B73EF15BAC5979C834FA187572F6E84E2142A38147ABD032FDE266358E81FAF8D38B1AD8FD0CF4A93676F4B575039A58F7995791C05E477051F5AF2B88CD2D821838894C52B106F20E2399A8C2B4C36229590E91FAD05D5A3432C614C46ADB22AE1BF0BC08B2EEA415E0FDCA26F731AC693BE8ADCE8C64B90E984EB8F87EB9B5176D28C0A92FC6C6B1B56D38D4FCA119EF22F1B9466833A3B3DED6583127ABDC2BC16BD92BF575ED0D866915C411FE231160D7F8A304641B8C3EEB8403050EF8E05A01EF32AD5EEC89748BBA350ACDA1D9BF6EB7A86680D053FA440CF4E56BAD35EBD02C96732A83E976D4EF2CBB26DBB6CAF60E2C4DE962A9083AF5E3C55AD17613FABB9B566D1BF5671741AC5B163F53A009EDF83126CD00BB6159FBC38E8704608D41E4092E06974FF0392E71AD6F19F3DB0BE844E1A63412E11DEF9F6DEDBC33306EF5D1AACC9E218AEAFDEA666F57B34070E86E475485AED4CEF26723E4729BA0E5DA2897C424E5BC5A424D00B3B978E0393648EC10CDD59948668373D9EEC58873642673EF0347A56B1A93E16757833942952DAA16C795B55EB26DA78610357451D0557B448CF555EDE962B42A581A9BC8A82A7AC44CF122DEEC560CF64DEFFD99A91FD72C0B63C60FBB0F733C913027B3F13B29E7E057EDA7757AD419C2DD1FE419C913D3A10675CE1CF2F9E4B0E7E03F75351199337AAAFF66C352F158133BD3573FEDD3EAC196E984377DE7EE9EA507E992491E3650016AE01720F2FCF075677AC7A772F9504EB2AC602C1E0F4B6188E98012C135B04C57D48AD3AEBD2A15723339038C095C58007E11A32549C57024395DB9867EA5F525F18A13026EB0F10FB20C1ABCA0B910C672F74BC2DF06BA522B4323C2DC8784BFA62C915DCC2906C0FB5A337E958ED2E269D977D0893D02499C99841553DD80487996E6E75DEB36A6E2BEFF02060D3F8EB58864ABF572DD05AC84A547AEB168252039657C2804B53A5732B38CA35C4BD2D4E8560065A9D0A0198F4ACBDF81962798A164DE3048BE6CDE1212718540A8E727D6EBF90E3053324E47801BC1FC8517BB5717E05E3F5F080E3CDE5E1D50F592A43A28D1BFDD1838DBF1E6B3CECF9BBB266E5439E551DD11AB888444F47A333431D82AAFC24120A8B08C685DF38BB282BE3A3243B16779A9BB2496E9E886320549710F1BEA7CABE90F47F49083C017661E808550BA78158215715A16A9E9A88E4CA9B9248A9D83510C92364B4C331964BE1E4D452CA952643728577524B2E3F1005720CC6789A6C381053491D3024AEF24683B21C45093469A768B401191A02D6C40D9E1FA48100C41B7F590075268E8991C330CF60BB46081ADB84A553C17B670148AE783504B4868B91E9C2F09E03AD01042A6B43818282E7FEA4502CAE1A29A87469236DBA9B14040558218582E7FEA490AFAE1A2128B43B13FDAE9B0878956C88DD80EEEF359B81AC7018A81CDDB7024EC9681262CDC00B3762A953946593318DD6CE3F4CC69AB0EEC92DD86EBD70C38479E75FAC258DF19E7DB36C1FDD1C501A63870395A801953DA128061B2894123CB870EEC509BA0208AC0171A2CEDC40AAC66B509A53B7E88B5592E4992A8EE0A236F99BB6681DEA5E295CB2DE9D939FE35107446BCFAE8E8485A06E6A91307CE083587153358BFC3408F50684BE35BD5B66DBD32F3285C958E05DB21324214A461B3F2346F3C5EF0CFB9FB752BFED3677FAE6BA1928EC2E760E74B6989E4AE13C66A9E81CCA079BCB6AAFDCEF3C96E645FB39D4373DF4FAD351A0B1B82C05FAC59C0213CBC70DA4FA7C3C082A15E63D23A8B02D3B2048DB743F08E242493918B0052DE865D1A11CA1ECCB3F124F3A5B781FA7CA0E27CA5058CA433D5902F9A79634986841891853664E950FE86469F225E61485A84D96A450D4824B36369363922DE8444F2351750DF31EE4684C96BA5C6A4E591197C992561477A0ADE0592C33A7AA08DD64092B8ACD6957719CA29E77A4FAB6D645B48FAD91FA4DBBEF8F9AF6FBD924FB51D899803D9610F3B925AD3C244F22967F3F3A8069BD6FFB0018F5A4770798A6BD7E7FE242E2F8EDA9368E4F4F938B73E38E80BA383F3DBD7630DE3758784797DA6C2C5CFB7D9886052D53ED9DF8F2C4A353794D204BCE680BAA8829AF6099DE5B33A6BD03EEBC37EAF9113D96DDE79A3A6E7B996A4ACAD4D2AF1328EBBDED28CF8C449F93CC3AA60799632B7B29C4F5B2EBFB4542A25DCB48578391364344F26D8B55CACDA8F4710BBEEC49EE576E7EC7447234D32AB655ECAA53BBC0D8F2377F446A95A09BF91E245A6051F51684DE134C100DF4A6AFB3708F9E1CCF0324E324717D85835EF50A093F7B03645C7844A88D39152D23B5A99A4D3B7801B1F30C54B9A58BD085AF53FB8FACD145767943FECA3E9F60B07F0ABDDF525CB08A5368FD29A761B58926EFEB958D92E73FDFC57B16D9EC36097EF1DF47DAEAC4BA8FF1D2BAB04E89B8777A04C3B463DAAA45C77DBC9CB1DB24BE97F526E6B6F7F70A0579C72AA1EE9A8A8A493A9394C89E604DC3C722795C4791DF2921E4B11BD81FF5A0DB2159AAE6D9877F1CE614AF2AEC0B7ACC130ABB01A667FC7176443B0C324DF7B1F9A97DD9EF166BDCAB082A94D5EB03DD9F44D80D1FAA87111C3F5AEFC8AAF209841EE82A1E3BE8976ACFE2D53D6EB01B55ED13079D4F3DF59B07BB71A97DF9205B7F3BBE7B60ACB2170D8F43655738A6DFED8E7730355ECA6DEF610B90B3D85B13ED2D5F7D373DEC9D25862B1532929CDD7B16F89E7A1A6819EC2D4FFCD0A9E19527E33019E143666119F7F79E72BF0F9AEE5D5AB3ED33BCBB02A7269AB077E0D4C49DC97D35DC190D99236A98097F0C79A1798ED68152DE87DC7D7451154796FBD936B7FD184094AB72074A621F1244BAC88963059159B6FA3160E8504AD0D008325682DE41067A2BFCC809E2DDF5A97738FFADF4A69AB088DE275F9D8D2FA79C8913276771D3955A93704FA309A6B6BB8EF03453634F9DE358978BDF988AAFEA26B306CDFAAA405793A9AFEA63757F757FB358AE96C689FCB579FCCA2E2E97BF2C56D7B7CD5D54CBB026D35F2BA9768F0034BF01A0ED4893EA5CFB4840F31B01DAEE3439C5433D222065F9AADE8EA8391E75E97547F960803231B83E2F5858167C0CE2913E0AA09C15A339E5D68E2282FB48F3FFBB0F985BBD8A88E2A34CF5EF3EDCC1D6ECAE69FDDD87DA6AADF793C02F073362E587F9AFDBB0E695789B8A0409CF0CA1C3A93D659D45F81415EA97C05151457078DF42045CAC135DC6C87B020EC2C5E4A22F7B6135BB412137D56BE82EC2FB146D5384870C83B5CFDD2D102DAEAEFFEC95029EE7C97D16C891F43104CCA6878700EFC39F52CF774BBEE70ABFBB8604510FF3CB343297885CAA6DDE4A4A77528EA48E502EBE52AB5DC160EB6362C97DB8042FB00B6F18B43770039CB7EAB24547A4792278B14FAE3CB0894190E434AAF6F827C6B01BBCFEF0379241EDC1C1700000', CAST(_UTF8'6.4.4' AS VARCHAR(32)))

