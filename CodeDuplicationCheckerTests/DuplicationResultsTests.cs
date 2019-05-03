using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodeDuplicationChecker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                new DuplicateCode()
                {
                    Instances = new List<DuplicateInstance> ()
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
                                    setTextContextParameters[0] = testContext;
                                    methodInfo.Invoke(methodInstance, setTextContextParameters);
                                    loadedInstances.Add(action.TestClass, methodInstance);
                                }
                                else
                                {
                                    methodInstance = loadedInstances[action.TestClass];
                                }"
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
                                }"
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
                                }"
                        }
                    }
                },
                new DuplicateCode()
                {
                    Instances = new List<DuplicateInstance> ()
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
                                }"
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
                                }"
                        }
                    }
                },
            };

            // Act
            duplicationResults.GenerateResultsFile(false);
        }
    }
}