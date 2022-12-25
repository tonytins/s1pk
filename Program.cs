using System;
using System.IO.Compression;
using System.Text.Json;
using Sims1Pkg;

var package = new TS1Package();

package.Extract($"{Environment.CurrentDirectory}/archive.s1pk");