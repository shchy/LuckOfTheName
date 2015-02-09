#load "code/Luck.csx"
#load "code/ILuckMapFactory.csx"
#load "code/LuckMapFactory.csx"

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


Require<NancyPack>()
	.At(1234)
//	//.Get("/", _ => "Hello world")
	.Host();
