using Microsoft.VisualStudio.TestTools.UnitTesting;
using forum_api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace forum_api.Services.Tests
{
    [TestClass()]
    public class WordFilterServiceTests
    {
        private string expextedText;
        private string expextedOKText;
        private IWordFilterService _wordfilterService;

        [TestInitialize]
        public void Initialize()
        {
            _wordfilterService = new WordFilterService();

            expextedText = "Nom de dieu de p****n de b****l de m***e de s***perie de c*nnard d enc*lé de ta mère C est aussi jouissif que de se torcher le c*l avec de la soie";
            expextedOKText = "Bonjour";
        }

        [TestMethod()]
        [DataRow("Nom de dieu de putain de bordel de merde de saloperie de connard d enculé de ta mère C est aussi jouissif que de se torcher le cul avec de la soie")]
        public void WordFilterSentenceTesti_TextOKAvecINSULT_ReturnTextCensure(string textATester)
        {
            string text = _wordfilterService.WordFilterSentence(textATester);

            Assert.AreEqual(text, expextedText);
        }
        [TestMethod()]
        [DataRow("Bonjour")]
        public void WordFilterSentenceTesti_TextOKSansINSULT_ReturnTextNonCensure(string textATester)
        {
            string text = _wordfilterService.WordFilterSentence(textATester);

            Assert.AreEqual(text, expextedOKText);
        }
    }
}