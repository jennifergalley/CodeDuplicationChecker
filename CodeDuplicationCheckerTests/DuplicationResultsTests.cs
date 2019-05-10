using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CodeDuplicationChecker.Tests
{
    [TestClass()]
    public class DuplicationResultsTests
    {
        DuplicationResults duplicationResults;

        [TestInitialize]
        public void TestInitialize()
        {
            duplicationResults = new DuplicationResults();
        }

        [TestMethod()]
        public void GenerateResultsFileTest()
        {
            // Arrange
            duplicationResults.CodeDuplicates = new List<DuplicateCode>()
            {
                type1.Copy(),
                type2.Copy(),
                type3.Copy()
            };

            // Act
            duplicationResults.GenerateResultsFile(false);
        }

        [TestMethod()]
        public void GenerateCodeHTML_Type1_Test()
        {
            // Arrange
            var copy = type1.Copy();

            // Act
            DuplicationResults.GenerateCodeHTML(copy);

            // Assert
            for (int i = 0; i < type1.Instances.Count; i++)
            {
                var expected = type2.Instances[i].CodeHtml.Trim();
                var actual = copy.Instances[i].CodeHtml.Trim();
                Assert.AreEqual(expected, actual, $"Instance {i} failed");
            }
        }

        [TestMethod()]
        public void GenerateCodeHTML_Type2_Test()
        {
            // Arrange
            var copy = type2.Copy();

            // Act
            DuplicationResults.GenerateCodeHTML(copy);

            // Assert
            for (int i = 0; i < type2.Instances.Count; i++)
            {
                var expected = type2.Instances[i].CodeHtml.Trim();
                var actual = copy.Instances[i].CodeHtml.Trim();
                Assert.AreEqual(expected, actual, $"Instance {i} failed");
            }
        }

        [TestMethod()]
        public void GenerateCodeHTML_Type3_Test()
        {
            // Arrange
            var copy = type3.Copy();

            // Act
            DuplicationResults.GenerateCodeHTML(copy);

            // Assert
            for (int i = 0; i < type3.Instances.Count; i++)
            {
                var expected = type2.Instances[i].CodeHtml.Trim();
                var actual = copy.Instances[i].CodeHtml.Trim();
                Assert.AreEqual(expected, actual, $"Instance {i} failed");
            }
        }

        #region TestCode
        private readonly DuplicateCode type1 = new DuplicateCode()
        {
            Instances = new List<DuplicateInstance>()
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
<span class='same'>    methodInfo = type.GetMethod(""SetTestContext"");</span>
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
<span class='diff'></span>
<span class='same'>    // Need to call SetTestContext(TestContext context) to pass test item setting and logger info. And use same test context for all the action modules in same test.</span>
<span class='same'>    methodInfo = type.GetMethod(""SetTestContext"");</span>
<span class='same'>    object[] setTextContextParameters = new object[1];</span>
<span class='diff'>    // New comment</span>
<span class='same'>    setTextContextParameters[0] = testContext;</span>
<span class='same'>    methodInfo.Invoke(methodInstance, setTextContextParameters);</span>
<span class='same'>    loadedInstances.Add(action.TestClass, methodInstance);</span>
<span class='same'>}</span>
<span class='same'>else</span>
<span class='same'>{</span>
<span class='same'>    methodInstance = loadedInstances[action.TestClass];</span>
<span class='same'>}</span>",
                },
                new DuplicateInstance ()
                {
                    Filename = "file2.cs",
                    StartLine = 55,
                    EndLine = 72,
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
<span class='same'>    methodInfo = type.GetMethod(""SetTestContext"");</span>
<span class='same'>    object[] setTextContextParameters = new object[1];</span>
<span class='same'>    setTextContextParameters[0] = testContext;</span>
<span class='diff'></span>
<span class='same'>    methodInfo.Invoke(methodInstance, setTextContextParameters);</span>
<span class='same'>    loadedInstances.Add(action.TestClass, methodInstance);</span>
<span class='same'>}</span>
<span class='same'>else</span>
<span class='same'>{</span>
<span class='same'>    methodInstance = loadedInstances[action.TestClass];</span>
<span class='same'>}</span>",
                }
            }
        };

        private readonly DuplicateCode type2 = new DuplicateCode()
        {
            Instances = new List<DuplicateInstance>()
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
            }
};

        private readonly DuplicateCode type3 = new DuplicateCode()
        {
            Instances = new List<DuplicateInstance>()
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
<span class='same'>public static List<TestItem.Action> UpdateActionsRunTimeValues(List<TestItem.Action> templateDefintion, TestContext testContext)</span>
<span class='same'>{</span>
<span class='same'>    XmlSerializer s = new XmlSerializer(typeof(List<TestItem.Action>));</span>
<span class='same'></span>
<span class='same'>    using (StringWriter outStream = new StringWriter())</span>
<span class='same'>    {</span>
<span class='same'>        // Here are some reordered lines</span>
<span class='diff'>        int j = 5 * 6;</span>
<span class='diff'>        int i = 3 * 5;</span>
<span class='diff'>        // This is an added line</span>
<span class='same'>        s.Serialize(outStream, templateDefintion);</span>
<span class='same'>        string input = outStream.ToString();</span>
<span class='same'>        input = ReplaceRunTimeValues(input, testContext, ref s);</span>
<span class='same'>        using (StringReader rdr = new StringReader(input))</span>
<span class='same'>        {</span>
<span class='same'>            templateDefintion = (List<TestItem.Action>)s.Deserialize(rdr);</span>
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
<span class='same'>public static List<TestItem.Action> UpdateActionsRunTimeValues(List<TestItem.Action> templateDefintion, TestContext testContext)</span>
<span class='same'>{</span>
<span class='same'>    XmlSerializer s = new XmlSerializer(typeof(List<TestItem.Action>));</span>
<span class='same'></span>
<span class='same'>    using (StringWriter outStream = new StringWriter())</span>
<span class='same'>    {</span>
<span class='same'>        // Here are some reordered lines</span>
<span class='diff'>        int i = 3 * 5;</span>
<span class='diff'>        int j = 5 * 6;</span>
<span class='same'>        s.Serialize(outStream, templateDefintion);</span>
<span class='same'>        string input = outStream.ToString();</span>
<span class='diff'>        // Here I have removed a line</span>
<span class='same'>        using (StringReader rdr = new StringReader(input))</span>
<span class='same'>        {</span>
<span class='same'>            templateDefintion = (List<TestItem.Action>)s.Deserialize(rdr);</span>
<span class='same'>        }</span>
<span class='same'>    }</span>
<span class='same'>    return templateDefintion;</span>
<span class='same'>}</span>"
                }
            }
        };
        #endregion
    }
}