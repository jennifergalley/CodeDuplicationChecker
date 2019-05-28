using Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CodeDuplicationChecker.UnitTests
{
    [TestClass()]
    public class VisualizeDiffsTests
    {
        [TestMethod()]
        public void GenerateResultsFile_Type1_Test()
        {
            // Act
            var success = VisualizeDiffs.TryGenerateResultsFile(type1, out var filePath, false);

            // Assert
            Assert.IsTrue(success);
        }

        [TestMethod()]
        public void GenerateResultsFile_Type2_Test()
        {
            // Act
            var success = VisualizeDiffs.TryGenerateResultsFile(type2, out var filePath, false);

            // Assert
            Assert.IsTrue(success);
        }

        [TestMethod()]
        public void GenerateResultsFile_Type3_Test()
        {
            // Act
            var success = VisualizeDiffs.TryGenerateResultsFile(type3, out var filePath, false);

            // Assert
            Assert.IsTrue(success);
        }

        [TestMethod()]
        public void GenerateCodeHTML_Type1_Test()
        {
            // Arrange
            var copy = new List<DuplicateInstance>();
            foreach (var instance in type1)
            {
                copy.Add(instance.Copy());
            }

            // Act
            VisualizeDiffs.GenerateCodeHTML(copy);

            // Assert
            for (int i = 0; i < type1.Count; i++)
            {
                var expected = type1[i].CodeHtml.Trim();
                var actual = copy[i].CodeHtml.Trim();
                Assert.AreEqual(expected, actual, $"Instance {i} failed");
            }
        }

        [TestMethod()]
        public void GenerateCodeHTML_Type2_Test()
        {
            // Arrange
            var copy = new List<DuplicateInstance>();
            foreach (var instance in type2)
            {
                copy.Add(instance.Copy());
            }

            // Act
            VisualizeDiffs.GenerateCodeHTML(copy);

            // Assert
            for (int i = 0; i < type2.Count; i++)
            {
                var expected = type2[i].CodeHtml.Trim();
                var actual = copy[i].CodeHtml.Trim();
                Assert.AreEqual(expected, actual, $"Instance {i} failed");
            }
        }

        [TestMethod()]
        public void GenerateCodeHTML_Type3_Test()
        {
            // Arrange
            var copy = new List<DuplicateInstance>();
            foreach (var instance in type3)
            {
                copy.Add(instance.Copy());
            }

            // Act
            VisualizeDiffs.GenerateCodeHTML(copy);

            // Assert
            for (int i = 0; i < type3.Count; i++)
            {
                var expected = type3[i].CodeHtml.Trim();
                var actual = copy[i].CodeHtml.Trim();
                Assert.AreEqual(expected, actual, $"Instance {i} failed");
            }
        }

        [TestMethod()]
        public void SplitCodeNewlines_Type1_Test()
        {
            foreach (var duplicate in type1)
            {
                // Arrange
                var code = duplicate.Code;

                // Act
                var result = VisualizeDiffs.SplitCodeNewlines(code);

                // Assert
                Assert.IsTrue(result.Count > 0);
            }
        }

        [TestMethod()]
        public void SplitCodeNewlines_Type2_Test()
        {
            foreach (var duplicate in type2)
            {
                // Arrange
                var code = duplicate.Code;

                // Act
                var result = VisualizeDiffs.SplitCodeNewlines(code);

                // Assert
                Assert.IsTrue(result.Count > 0);
            }
        }

        [TestMethod()]
        public void SplitCodeNewlines_Type3_Test()
        {
            foreach (var duplicate in type3)
            {
                // Arrange
                var code = duplicate.Code;

                // Act
                var result = VisualizeDiffs.SplitCodeNewlines(code);

                // Assert
                Assert.IsTrue(result.Count > 0);
            }
        }

        #region TestCode
        private readonly List<DuplicateInstance> type1 = new List<DuplicateInstance>()
        {
            new DuplicateInstance ()
            {
                Filename = "file1.cs",
                StartLine = 80,
                EndLine = 97,
                Code = @"
if (!loadedInstances.ContainsKey(action.TestClass))
{
    // Get the Constructor
    ConstructorInfo methodConstructor = type.GetConstructor(Type.EmptyTypes);
    methodInstance = methodConstructor.Invoke(new object[] { });

    // Need to call SetTestContext(TestContext context) to pass test item setting and logger info. And use same test context for all the action modules in same test.
    methodInfo = type.GetMethod(""SetTestContext"");
    object[] setTextContextParameters = new object[1];
    // New comment
    setTextContextParameters[0] = testContext;
    methodInfo.Invoke(methodInstance, setTextContextParameters);
    loadedInstances.Add(action.TestClass, methodInstance); // some other new comment
}
else
{
    methodInstance = loadedInstances[action.TestClass];
}",
                CodeHtml =@"
<span class='same'>if (!loadedInstances.ContainsKey(action.TestClass))</span>
<span class='same'>{</span>
<span class='same'>    // Get the Constructor</span>
<span class='same'>    ConstructorInfo methodConstructor = type.GetConstructor(Type.EmptyTypes);</span>
<span class='same'>    methodInstance = methodConstructor.Invoke(new object[] { });</span>
<span class='same'></span>
<span class='same'>    // Need to call SetTestContext(TestContext context) to pass test item setting and logger info. And use same test context for all the action modules in same test.</span>
<span class='same'>    methodInfo = type.GetMethod(&quot;SetTestContext&quot;);</span>
<span class='same'>    object[] setTextContextParameters = new object[1];</span>
<span class='diff'>    // New comment</span>
<span class='same'>    setTextContextParameters[0] = testContext;</span>
<span class='same'>    methodInfo.Invoke(methodInstance, setTextContextParameters);</span>
<span class='diff'>    loadedInstances.Add(action.TestClass, methodInstance); // some other new comment</span>
<span class='same'>}</span>
<span class='same'>else</span>
<span class='same'>{</span>
<span class='same'>    methodInstance = loadedInstances[action.TestClass];</span>
<span class='same'>}</span>",
            },
            new DuplicateInstance ()
            {
                Filename = "file1.cs",
                StartLine = 100,
                EndLine = 117,
                Code = @"
if (!loadedInstances.ContainsKey(action.TestClass))
{
    // Get the Constructor
    ConstructorInfo methodConstructor = type.GetConstructor(Type.EmptyTypes);
    methodInstance = methodConstructor.Invoke(new object[] { });

    // Need to call SetTestContext(TestContext context) to pass test item setting and logger info. And use same test context for all the action modules in same test.
    methodInfo = type.GetMethod(""SetTestContext"");
    object[] setTextContextParameters = new object[1];
    setTextContextParameters[0] = testContext;
    methodInfo.Invoke(methodInstance, setTextContextParameters);
    loadedInstances.Add(action.TestClass, methodInstance);
}
else
{
    methodInstance = loadedInstances[action.TestClass];
}",
                CodeHtml = @"
<span class='same'>if (!loadedInstances.ContainsKey(action.TestClass))</span>
<span class='same'>{</span>
<span class='same'>    // Get the Constructor</span>
<span class='same'>    ConstructorInfo methodConstructor = type.GetConstructor(Type.EmptyTypes);</span>
<span class='same'>    methodInstance = methodConstructor.Invoke(new object[] { });</span>
<span class='same'></span>
<span class='same'>    // Need to call SetTestContext(TestContext context) to pass test item setting and logger info. And use same test context for all the action modules in same test.</span>
<span class='same'>    methodInfo = type.GetMethod(&quot;SetTestContext&quot;);</span>
<span class='same'>    object[] setTextContextParameters = new object[1];</span>
<span class='same'>    setTextContextParameters[0] = testContext;</span>
<span class='same'>    methodInfo.Invoke(methodInstance, setTextContextParameters);</span>
<span class='diff'>    loadedInstances.Add(action.TestClass, methodInstance);</span>
<span class='same'>}</span>
<span class='same'>else</span>
<span class='same'>{</span>
<span class='same'>    methodInstance = loadedInstances[action.TestClass];</span>
<span class='same'>}</span>",
            }
        };

        private readonly List<DuplicateInstance> type2 = new List<DuplicateInstance>()
        {
            new DuplicateInstance ()
            {
                Filename = "file5.cs",
                StartLine = 14,
                EndLine = 35,
                Code = @"
/// <summary>
/// Update the RunTime Value in the *Tests.xml 
/// </summary>
/// <param name=""templateDefintion""></param>
/// <returns></returns>
public static List<TestItem.Action> UpdateActionsRunTimeValues(List<TestItem.Action> templateDefintion, TestContext testContext)
{
    XmlSerializer s = new XmlSerializer(typeof(List<TestItem.Action>));

    using (StringWriter outStream = new StringWriter())
    {
        s.Serialize(outStream, templateDefintion);
        string input = outStream.ToString();
        input = ReplaceRunTimeValues(input, testContext, ref s);
        using (StringReader rdr = new StringReader(input))
        {
            templateDefintion = (List<TestItem.Action>)s.Deserialize(rdr);
        }
    }
    return templateDefintion;
}",
                CodeHtml = @"
<span class='same'>/// &lt;summary&gt;</span>
<span class='same'>/// Update the RunTime Value in the *Tests.xml</span>
<span class='same'>/// &lt;/summary&gt;</span>
<span class='same'>/// &lt;param name=&quot;templateDefintion&quot;&gt;&lt;/param&gt;</span>
<span class='same'>/// &lt;returns&gt;&lt;/returns&gt;</span>
<span class='diff'>public static List&lt;TestItem.Action&gt; UpdateActionsRunTimeValues(List&lt;TestItem.Action&gt; templateDefintion, TestContext testContext)</span>
<span class='same'>{</span>
<span class='diff'>    XmlSerializer s = new XmlSerializer(typeof(List&lt;TestItem.Action&gt;));</span>
<span class='same'></span>
<span class='same'>    using (StringWriter outStream = new StringWriter())</span>
<span class='same'>    {</span>
<span class='same'>        s.Serialize(outStream, templateDefintion);</span>
<span class='same'>        string input = outStream.ToString();</span>
<span class='same'>        input = ReplaceRunTimeValues(input, testContext, ref s);</span>
<span class='same'>        using (StringReader rdr = new StringReader(input))</span>
<span class='same'>        {</span>
<span class='diff'>            templateDefintion = (List&lt;TestItem.Action&gt;)s.Deserialize(rdr);</span>
<span class='same'>        }</span>
<span class='same'>    }</span>
<span class='same'>    return templateDefintion;</span>
<span class='same'>}</span>",
            },
            new DuplicateInstance ()
            {
                Filename = "file5.cs",
                StartLine = 40,
                EndLine = 61,
                Code = @"
/// <summary>
/// Update the RunTime Value in the *Tests.xml 
/// </summary>
/// <param name=""templateDefintion""></param>
/// <returns></returns>
public static TestItem.TemplateItemsDefinition UpdateTemplatesRunTimeValues(TestItem.TemplateItemsDefinition templateDefintion, TestContext testContext)
{
    XmlSerializer s = new XmlSerializer(typeof(TestItem.TemplateItemsDefinition));

    using (StringWriter outStream = new StringWriter())
    {
        s.Serialize(outStream, templateDefintion);
        string input = outStream.ToString();
        input = ReplaceRunTimeValues(input, testContext, ref s);
        using (StringReader rdr = new StringReader(input))
        {
            templateDefintion = (TestItem.TemplateItemsDefinition)s.Deserialize(rdr);
        }
    }
    return templateDefintion;
}",
                CodeHtml = @"
<span class='same'>/// &lt;summary&gt;</span>
<span class='same'>/// Update the RunTime Value in the *Tests.xml</span>
<span class='same'>/// &lt;/summary&gt;</span>
<span class='same'>/// &lt;param name=&quot;templateDefintion&quot;&gt;&lt;/param&gt;</span>
<span class='same'>/// &lt;returns&gt;&lt;/returns&gt;</span>
<span class='diff'>public static TestItem.TemplateItemsDefinition UpdateTemplatesRunTimeValues(TestItem.TemplateItemsDefinition templateDefintion, TestContext testContext)</span>
<span class='same'>{</span>
<span class='diff'>    XmlSerializer s = new XmlSerializer(typeof(TestItem.TemplateItemsDefinition));</span>
<span class='same'></span>
<span class='same'>    using (StringWriter outStream = new StringWriter())</span>
<span class='same'>    {</span>
<span class='same'>        s.Serialize(outStream, templateDefintion);</span>
<span class='same'>        string input = outStream.ToString();</span>
<span class='same'>        input = ReplaceRunTimeValues(input, testContext, ref s);</span>
<span class='same'>        using (StringReader rdr = new StringReader(input))</span>
<span class='same'>        {</span>
<span class='diff'>            templateDefintion = (TestItem.TemplateItemsDefinition)s.Deserialize(rdr);</span>
<span class='same'>        }</span>
<span class='same'>    }</span>
<span class='same'>    return templateDefintion;</span>
<span class='same'>}</span>",
            }
        };

        private readonly List<DuplicateInstance> type3 = new List<DuplicateInstance>()
        {
            new DuplicateInstance ()
            {
                Filename = "file9.cs",
                StartLine = 14,
                EndLine = 35,
                Code = @"
public static List<TestItem.Action> UpdateActionsRunTimeValues(List<TestItem.Action> templateDefintion, TestContext testContext)
{
    XmlSerializer s = new XmlSerializer(typeof(List<TestItem.Action>));

    using (StringWriter outStream = new StringWriter())
    {
        // Here are some reordered lines
        int j = 5 * 6;
        int i = 3 * 5;
        // This is an added comment
        s.Serialize(outStream, templateDefintion);
        string input = outStream.ToString();
        input = ReplaceRunTimeValues(input, testContext, ref s);
        using (StringReader rdr = new StringReader(input))
        {
            templateDefintion = (List<TestItem.Action>)s.Deserialize(rdr);
        }
    }
    return templateDefintion;
}",
                CodeHtml = @"
<span class='diff'>public static List&lt;TestItem.Action&gt; UpdateActionsRunTimeValues(List&lt;TestItem.Action&gt; templateDefintion, TestContext testContext)</span>
<span class='same'>{</span>
<span class='diff'>    XmlSerializer s = new XmlSerializer(typeof(List&lt;TestItem.Action&gt;));</span>
<span class='same'></span>
<span class='same'>    using (StringWriter outStream = new StringWriter())</span>
<span class='same'>    {</span>
<span class='same'>        // Here are some reordered lines</span>
<span class='same'>        int j = 5 * 6;</span>
<span class='same'>        int i = 3 * 5;</span>
<span class='diff'>        // This is an added comment</span>
<span class='same'>        s.Serialize(outStream, templateDefintion);</span>
<span class='same'>        string input = outStream.ToString();</span>
<span class='diff'>        input = ReplaceRunTimeValues(input, testContext, ref s);</span>
<span class='same'>        using (StringReader rdr = new StringReader(input))</span>
<span class='same'>        {</span>
<span class='diff'>            templateDefintion = (List&lt;TestItem.Action&gt;)s.Deserialize(rdr);</span>
<span class='same'>        }</span>
<span class='same'>    }</span>
<span class='same'>    return templateDefintion;</span>
<span class='same'>}</span>"
            },
            new DuplicateInstance ()
            {
                Filename = "file7.cs",
                StartLine = 40,
                EndLine = 61,
                Code = @"
public static TestItem.TemplateItemsDefinition UpdateTemplatesRunTimeValues(TestItem.TemplateItemsDefinition templateDefintion, TestContext testContext)
{
    XmlSerializer s = new XmlSerializer(typeof(TestItem.TemplateItemsDefinition));

    using (StringWriter outStream = new StringWriter())
    {
        // Here are some reordered lines
        int i = 3 * 5;
        int j = 5 * 6;
        s.Serialize(outStream, templateDefintion);
        string input = outStream.ToString();
        // Here I have removed a line
        using (StringReader rdr = new StringReader(input))
        {
            templateDefintion = (TestItem.TemplateItemsDefinition)s.Deserialize(rdr);
        }
    }

    return templateDefintion;
}",
                CodeHtml = @"
<span class='diff'>public static TestItem.TemplateItemsDefinition UpdateTemplatesRunTimeValues(TestItem.TemplateItemsDefinition templateDefintion, TestContext testContext)</span>
<span class='same'>{</span>
<span class='diff'>    XmlSerializer s = new XmlSerializer(typeof(TestItem.TemplateItemsDefinition));</span>
<span class='same'></span>
<span class='same'>    using (StringWriter outStream = new StringWriter())</span>
<span class='same'>    {</span>
<span class='same'>        // Here are some reordered lines</span>
<span class='same'>        int i = 3 * 5;</span>
<span class='same'>        int j = 5 * 6;</span>
<span class='same'>        s.Serialize(outStream, templateDefintion);</span>
<span class='same'>        string input = outStream.ToString();</span>
<span class='diff'>        // Here I have removed a line</span>
<span class='same'>        using (StringReader rdr = new StringReader(input))</span>
<span class='same'>        {</span>
<span class='diff'>            templateDefintion = (TestItem.TemplateItemsDefinition)s.Deserialize(rdr);</span>
<span class='same'>        }</span>
<span class='same'>    }</span>
<span class='same'></span>
<span class='same'>    return templateDefintion;</span>
<span class='same'>}</span>"
            }
        };
        #endregion
    }
}