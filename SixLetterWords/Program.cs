// See https://aka.ms/new-console-template for more information

using LetterWord.Services;

var sixLetterWordService = new LetterWordService(6);
sixLetterWordService.RunCode("input2.txt");