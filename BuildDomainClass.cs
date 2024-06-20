using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Security.Policy;
using CRA.ModelLayer.Core;
using CRA.ModelLayer.Strategy;

namespace CRA.ModelLayer.DCC
{
    class BuildDomainClass
    {

        internal CodeCompileUnit WriteDomainClass(Form1 fr,
                                      DataSet d,
                                      List<CodeCommentStatement> commentsToClass)
        {

            #region Fields

            string _params = String.Empty;
            string _paramsFileNameExtension = String.Empty;
            bool _IsParametersClass = false;

            CodeMemberProperty p;
            CodeTypeReference reference;
            CodeComment comment;
            CodeCommentStatement commentStatement;

            #endregion

            // Create a new CodeCompileUnit to contain the program graph
            CodeCompileUnit compileUnit = new CodeCompileUnit();

            #region Namespace and imports

            // Generate code to define namespace and imports
            // Declare  namespace chosen 
            CodeNamespace DomainClass = new CodeNamespace(fr.txtNameSpace.Text);
            
            // Add the new namespace import for the System namespace.
            DomainClass.Imports.Add(new CodeNamespaceImport("System"));
            DomainClass.Imports.Add(new CodeNamespaceImport("System.Collections.Generic"));
            DomainClass.Imports.Add(new CodeNamespaceImport("System.Reflection")); //added to read/write Xml value files
            
            DomainClass.Imports.Add(new CodeNamespaceImport("CRA.ModelLayer.Core"));
            DomainClass.Imports.Add(new CodeNamespaceImport("CRA.ModelLayer.ParametersManagement"));
            DomainClass.Imports.Add(new CodeNamespaceImport("BioMA.Utilities.NetFramework"));
            // If parameters class
            if (fr.CreateParametersClass)
            {
                DomainClass.Imports.Add(new CodeNamespaceImport("System.IO")); //added to read/write Xml value files
                _params = " and the code to load/write parameters from an XML file (MPE)";
                _paramsFileNameExtension = "_Parameters";
                _IsParametersClass = true;
            }

            // Add the new namespace to the compile unit.
            compileUnit.Namespaces.Add(DomainClass);

            #endregion

            #region Reference to this application which generates code

            // reference to file TXT/XML which is origin of this class 
            DomainClass.Comments.Add(commentsToClass[0]);
            DomainClass.Comments.Add(commentsToClass[1]); // web portal
            DomainClass.Comments.Add(commentsToClass[2]); // DCC
            
            if (commentsToClass.Count == 8)
            {
                DomainClass.Comments.Add(commentsToClass[0]);
                DomainClass.Comments.Add(commentsToClass[4]); //Author
                DomainClass.Comments.Add(commentsToClass[5]);
                DomainClass.Comments.Add(commentsToClass[6]);
                DomainClass.Comments.Add(commentsToClass[7]);
            }
            DomainClass.Comments.Add(commentsToClass[0]);
            DomainClass.Comments.Add(commentsToClass[3]);  // Date
            DomainClass.Comments.Add(commentsToClass[0]);


            #endregion

            #region Class declaration

            // Generate code to declare class

            // Declare a new type called txtClassName.Text
            CodeTypeDeclaration Class = new CodeTypeDeclaration(fr.txtClassName.Text + _paramsFileNameExtension);
            string _ClassName = fr.txtClassName.Text + _paramsFileNameExtension;
            Class.IsClass = true;
            //CodeAttributeDeclaration codeAttrDecl =
            //new CodeAttributeDeclaration("Serializable");
            Class.CustomAttributes.Add(new CodeAttributeDeclaration("Serializable")); 

            Class.TypeAttributes = System.Reflection.TypeAttributes.Public;
            if (fr.LoadDomainClassFromInterfacesDllAndExtendIt)
            {
                string _ValueClass = fr.lblDomainClassToBeExtended.Text;
                _ValueClass = _ValueClass.Substring(0, _ValueClass.Length - 7);
                Class.BaseTypes.Add(_ValueClass);
            }
            //Class.BaseTypes.Add("IDomainClass");
            Class.BaseTypes.Add("ICloneable");
            // 07/06/2012 - DFa added if
            if (fr.CreateParametersClass)
            {
                Class.BaseTypes.Add("IParameters");
            }
            else
            {
                Class.BaseTypes.Add("IDomainClass");
            }
            // Add the new type to the namespace's type collection.
            DomainClass.Types.Add(Class);

            // class description
            CodeComment commentClass2 = new CodeComment(
                "<summary>" + fr.txtClassName.Text + " Domain class contains the accessors to values" + _params + "</summary>",
                true);
            CodeCommentStatement classCommentStatement2 = new CodeCommentStatement(commentClass2);
            Class.Comments.Add(classCommentStatement2);

            #endregion

            #region Constructors

            // Generate code for constructors

            //if (_IsParametersClass)
            //{
            //    // Defines constructors

            //    CodeConstructor constructor2 = new CodeConstructor();
            //    constructor2.StartDirectives.Add(new CodeRegionDirective(CodeRegionMode.Start, "Constructors"));

            //    // Access a record: File path and parameters key
            //    CodeComment commentClass3 = new CodeComment("<summary>Constructor to load from XML</summary>", true);
            //    CodeCommentStatement classCommentStatement3 = new CodeCommentStatement(commentClass3);
            //    CodeParameterDeclarationExpression param1 =
            //        new CodeParameterDeclarationExpression("System.String", "xmlPath");
            //    CodeParameterDeclarationExpression param2 =
            //        new CodeParameterDeclarationExpression("System.String", "parameterKey");
            //    constructor2.Comments.Add(classCommentStatement3);
            //    constructor2.Attributes = MemberAttributes.Public;
            //    constructor2.Parameters.Add(param1);
            //    constructor2.Parameters.Add(param2);
            //    constructor2.Statements.Add(new CodeSnippetExpression("if (xmlPath != String.Empty) { xmlPath += @" + (char)34 + (char)92 + (char)34 + ";}"));
            //    constructor2.Statements.Add(new CodeSnippetExpression("_path = xmlPath + _file"));  
            //    constructor2.Statements.Add(new CodeSnippetExpression("_key = parameterKey"));
            //    constructor2.Statements.Add(new CodeSnippetExpression("GetValues()"));

            //    Class.Members.Add(constructor2);

            //    // Set (and optionally save): VarInfo collection and parameters key  
            //    CodeConstructor constructor3 = new CodeConstructor();
            //    CodeComment commentClass4 = new CodeComment("<summary>Constructor to set and possibly save to XML</summary>", true);
            //    CodeCommentStatement classCommentStatement4 = new CodeCommentStatement(commentClass4);
            //    param1 =
            //        new CodeParameterDeclarationExpression("System.String", "xmlPath");
            //    param2 =
            //        new CodeParameterDeclarationExpression("System.String", "parameterKey");
            //    CodeParameterDeclarationExpression param3 =
            //        new CodeParameterDeclarationExpression("IEnumerable<VarInfo>", "parametersVarInfo");
            //    CodeParameterDeclarationExpression param4 =
            //        new CodeParameterDeclarationExpression("System.Boolean", "saveOnXml");
            //    constructor3.Comments.Add(classCommentStatement4);
            //    constructor3.Attributes = MemberAttributes.Public;
            //    constructor3.Parameters.Add(param1);
            //    constructor3.Parameters.Add(param2);
            //    constructor3.Parameters.Add(param3);
            //    constructor3.Parameters.Add(param4);
            //    constructor3.Statements.Add(new CodeSnippetExpression("if (xmlPath != String.Empty) { xmlPath += @" + (char)34 + (char)92 + (char)34 + ";}"));
            //    constructor3.Statements.Add(new CodeSnippetExpression("_path = xmlPath + _file"));
            //    constructor3.Statements.Add(new CodeSnippetExpression("_key = parameterKey"));
            //    constructor3.Statements.Add(new CodeSnippetExpression("_parVarInfo = parametersVarInfo"));

            //    // Set class properties to VarInfo values passed
            //    constructor3.Statements.Add(new CodeSnippetExpression("if (parametersVarInfo.Count > 0) { SetValues(); }"));
            //    // Overwrites Xml item with values passed
            //    constructor3.Statements.Add(new CodeSnippetExpression("if (saveOnXml) {SaveValues();}"));

            //    Class.Members.Add(constructor3);
            //}
            // No parameters
            CodeConstructor constructor = new CodeConstructor();
            CodeComment commentClass = new CodeComment("<summary>No parameters constructor</summary>", true);
            CodeCommentStatement classCommentStatement = new CodeCommentStatement(commentClass);
            constructor.Comments.Add(classCommentStatement);
            constructor.Attributes = MemberAttributes.Public;
            constructor.StartDirectives.Add(new CodeRegionDirective(CodeRegionMode.Start, "Constructor"));
            constructor.EndDirectives.Add(new CodeRegionDirective(CodeRegionMode.End, String.Empty));
            if (_IsParametersClass || (!fr.LoadDomainClassFromInterfacesDllAndExtendIt))
                constructor.Statements.Add(new CodeSnippetExpression("_parametersIO = new ParametersIO(this)"));
            if (_IsParametersClass)
            {
                constructor.Statements.Add(new CodeSnippetExpression(
                    "string filePath = Path.GetDirectoryName(Assembly.GetAssembly(this.GetType()).Location) + Path.DirectorySeparatorChar + " +
                    (char)34 + fr.txtNameSpace.Text + "_" + fr.txtClassName.Text + ".XML" + (char)34));
                constructor.Statements.Add(new CodeSnippetExpression("XmlRW _xmlRW = new XmlRW(filePath)"));
                constructor.Statements.Add(new CodeSnippetExpression("_parametersIO.Reader = _xmlRW"));
                constructor.Statements.Add(new CodeSnippetExpression("_parametersIO.Writer = _xmlRW"));
            }
            Class.Members.Add(constructor);

            #endregion

            #region Properties

            DataTable dt = d.Tables["VarInfoVariables"];

            // DF 24/4/2020 added Strategy - begin
            DataRow strategyRow = null;
            if (fr.CreateParametersClass)
            {
                var alfredo = dt.AsEnumerable().Select(row => row.Field<string>("name")).Cast<string>().ToList();
                //if (dt.Columns.Contains("Strategy"))
                if (dt.AsEnumerable().Select(row => row.Field<string>("name")).Cast<string>().ToList().Contains("Strategy"))
                {
                    throw new DCCException("Fields cannot be named 'Strategy'");
                }
                strategyRow = dt.NewRow();
                strategyRow.ItemArray = new object[] { "Strategy", 0, 0, 0, "", "IStrategy", "Instance of the Strategy to link to", "" };
                dt.Rows.Add(strategyRow);
            }
            // DF 24/4/2020 added Strategy - end

            //local
            CodeMemberField m;
            CodeMemberProperty p1;
            CodeTypeReference r;
            CodeComment commentProperty2;
            CodeCommentStatement propertyCommentStatement2;
            int _numero = dt.Rows.Count - 1;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                VarInfoAttributes v = new VarInfoAttributes(dt.Rows[i]);
                // fields

                string declare = "_" + v.name;
                string type = v.type;
                int size = 0;
                // DF 24/4/2020 added Strategy - begin
                VarInfoValueTypes varInfoValueType = null;
                if (!v.name.Equals("Strategy"))
                {
                    varInfoValueType = VarInfoValueTypes.GetVarInfoValueType(v.type, out size);
                }
                switch (fr.cmbLanguage.Text)
                {
                    case "C#":
                        ////add call for constructor of generics
                        //if ((v.type.EndsWith(">")) || 
                        //    (type.ToLower() != "double" &&
                        //     type.ToLower() != "int" && 
                        //     type.ToLower() != "bool" &&
                        //     type.ToLower() != "string"))
                        //{
                        //    declare = "_" + v.name + " = new " + v.type + "()";
                        //}
                        ////add call for constructor of arrays 1 dimension
                        //if (v.type.EndsWith("]") && !v.type.Contains(","))
                        //{
                        //    declare = "_" + v.name + " = new " + v.type;
                        //    type = v.type.Substring(0, v.type.IndexOf("[")) + "[]";
                        //}
                        ////add call for constructor of arrays 2 dimension
                        //if (v.type.EndsWith("]") && v.type.Contains(","))
                        //{
                        //    declare = "_" + v.name + " = new " + v.type;
                        //    type = v.type.Substring(0, v.type.IndexOf("[")) + "[,]";
                        //}
                        string constructingString = varInfoValueType?.Converter.GetConstructingString(size);
                        if (constructingString != null) declare += " = " + constructingString;
                        break;
                    case "VB":
                        //TODO:implement specific sintax for VB
                        break;
                    case "J#":
                        //TODO: implement specific sintax for J#
                        break;
                }
                //m = new CodeMemberField(" " + type, declare);
                if (varInfoValueType != null)
                {
                    m = new CodeMemberField(varInfoValueType.TypeForCurrentValue, declare);
                }
                else
                {
                    m = new CodeMemberField(typeof(IStrategy), declare);
                }
                // Add region for private fields (start to first, end to last)
                if (i == 0)
                {
                    m.StartDirectives.Add(new CodeRegionDirective(CodeRegionMode.Start,
                                                                   "Private fields"));
                }
                if (i == _numero)
                {
                    m.EndDirectives.Add(new CodeRegionDirective(CodeRegionMode.End,
                                          String.Empty));
                }
                //				
                m.Attributes = System.CodeDom.MemberAttributes.Private;
                Class.Members.Add(m);
                // properties
                p1 = new CodeMemberProperty();
                //add region for private fields (start to first, end to last)
                if (i == 0)
                {
                    p1.StartDirectives.Add(new CodeRegionDirective(CodeRegionMode.Start,
                                                                   "Public properties"));
                }
                if (i == _numero)
                {
                    p1.EndDirectives.Add(new CodeRegionDirective(CodeRegionMode.End,
                                          String.Empty));
                }
                //
                p1.Name = v.name;
                p1.GetStatements.Add(new CodeMethodReturnStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "_" + v.name)));
                p1.SetStatements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "_" + v.name), new CodePropertySetValueReferenceExpression()));

                /*davidefuma aggiunta chiamata evento ParameterClassPropertyValueSet START*/
                if (fr.CreateParametersClass && (varInfoValueType != null))//event present only in parameter classes
                {
                    CodeConditionStatement codeIf = new CodeConditionStatement(new CodeSnippetExpression("ParameterClassPropertyValueSet != null"), new CodeSnippetStatement("\t\t\t\t\tParameterClassPropertyValueSet(\"" + v.name + "\",value);"));

                    p1.SetStatements.Add(codeIf);
                }
                /*davidefuma aggiunta chiamata evento END*/

                p1.Attributes = MemberAttributes.Public | MemberAttributes.Final;
                if (varInfoValueType != null)
                {
                    r = new CodeTypeReference(varInfoValueType.TypeForCurrentValue);
                    p1.Type = r;
                } else
                {
                    p1.Type = new CodeTypeReference(typeof(IStrategy));
                }
                //description
                commentProperty2 = new CodeComment(
                    "<summary>" + v.description + "</summary>",
                    true);
                propertyCommentStatement2 = new CodeCommentStatement(commentProperty2);
                p1.Comments.Add(propertyCommentStatement2);
                Class.Members.Add(p1);
            }

            #endregion

            #region IDomainClass members

            // Generate code to implement IDomainClass members



            p = new CodeMemberProperty();

            p.StartDirectives.Add(new CodeRegionDirective(CodeRegionMode.Start,"IDomainClass members"));



            // Description
            p.GetStatements.Add(new CodeMethodReturnStatement(new CodeArgumentReferenceExpression((char)34 + fr.txtDescription.Text + (char)34)));
            p.Name = "Description";
            //p.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            if (fr.LoadDomainClassFromInterfacesDllAndExtendIt)
            {
                p.Attributes = MemberAttributes.Public | MemberAttributes.Override;
            }
            else
            {
                p.Attributes = MemberAttributes.Public;
            }
            reference = new CodeTypeReference(" string");
            p.Type = reference;



            //description
            comment = new CodeComment(
                "<summary>" + "Domain Class description" + "</summary>",
                true);
            commentStatement = new CodeCommentStatement(comment);
            p.Comments.Add(commentStatement);
            Class.Members.Add(p);


            /* 3/sept/2012 davide fumagalli - implementation of IDomainClass.ClearValues method START*/
            #region ClearValues implementation



            CodeMemberMethod methodClearValues = new CodeMemberMethod();
            comment = new CodeComment("<summary>Clears the values of the properties of the domain class by using the default value for the type of each property (e.g '0' for numbers, 'the empty string' for strings, etc.)</summary>", true);
            commentStatement = new CodeCommentStatement(comment);
            methodClearValues.Comments.Add(commentStatement);
            //methodClearValues.Attributes = MemberAttributes.Public;
            if (fr.LoadDomainClassFromInterfacesDllAndExtendIt)
            {
                methodClearValues.Attributes = MemberAttributes.Public | MemberAttributes.Override;
            }
            else
            {
                methodClearValues.Attributes = MemberAttributes.Public;
            }
            CodeTypeReference codref1 = new CodeTypeReference("Boolean");
            methodClearValues.ReturnType = codref1;
            methodClearValues.Name = "ClearValues";

            if (fr.LoadDomainClassFromInterfacesDllAndExtendIt)
            {
                methodClearValues.Statements.Add(new CodeSnippetExpression("bool baseOK = base.ClearValues()"));
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                VarInfoAttributes v = new VarInfoAttributes(dt.Rows[i]);
                // fields

                string declare = "_" + v.name;
                string type = v.type;
                int size;
                if (v.name.Equals("Strategy")) continue;
                VarInfoValueTypes varInfoValueType = VarInfoValueTypes.GetVarInfoValueType(v.type, out size);
                switch (fr.cmbLanguage.Text)
                {
                    case "C#":

                        string constructingString = varInfoValueType.Converter.GetConstructingString(size);
                        if (constructingString != null) declare += " = " + constructingString;
                        else declare += " = default(" + varInfoValueType.TypeForCurrentValue.FullName + ")";
                        break;
                    case "VB":
                        //TODO:implement specific sintax for VB
                        break;
                    case "J#":
                        //TODO: implement specific sintax for J#
                        break;
                }

                methodClearValues.Statements.Add(new CodeSnippetExpression(declare));
            }

            methodClearValues.Statements.Add(new CodeCommentStatement("Returns true if everything is ok"));
            if (fr.LoadDomainClassFromInterfacesDllAndExtendIt)
            {
                methodClearValues.Statements.Add(new CodeSnippetExpression("return baseOK && true"));
            }
            else
            {
                methodClearValues.Statements.Add(new CodeSnippetExpression("return true"));
            }
            Class.Members.Add(methodClearValues);


            #endregion
            /* 3/sept/2012 davide fumagalli - implementation of IDomainClass.ClearValues method END*/

            // URL
            p = new CodeMemberProperty();
            p.GetStatements.Add(new CodeMethodReturnStatement(new CodeArgumentReferenceExpression((char)34 + fr.txtURL.Text + (char)34)));
            p.Name = "URL";
            //p.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            if (fr.LoadDomainClassFromInterfacesDllAndExtendIt)
            {
                p.Attributes = MemberAttributes.Public | MemberAttributes.Override;
            }
            else
            {
                p.Attributes = MemberAttributes.Public;
            }
            reference = new CodeTypeReference(" string");
            p.Type = reference;


            //description
            comment = new CodeComment(
                "<summary>" + "Domain Class URL" + "</summary>",
                true);
            commentStatement = new CodeCommentStatement(comment);
            p.Comments.Add(commentStatement);
            Class.Members.Add(p);

            // PropertiesDescription
            if (!fr.LoadDomainClassFromInterfacesDllAndExtendIt)
            {
                p = new CodeMemberProperty();
                string excludedTypes = "typeof(IDomainClass)";
                if (_IsParametersClass)
                {
                    excludedTypes += ", typeof(IParameters)";
                }
                p.GetStatements.Add(new CodeSnippetExpression("return _parametersIO.GetCachedProperties(" + excludedTypes + ")"));
                p.Name = "PropertiesDescription";
                //p.Attributes = MemberAttributes.Public | MemberAttributes.Final;
                p.Attributes = MemberAttributes.Public;
                reference = new CodeTypeReference("IDictionary<string, PropertyInfo>");
                p.Type = reference;
                //description
                comment = new CodeComment(
                    "<summary>Domain Class Properties</summary>",
                    true);
                commentStatement = new CodeCommentStatement(comment);
                p.Comments.Add(commentStatement);
                Class.Members.Add(p);
            }

            // Strategy used
            //p = new CodeMemberProperty();
            //p.GetStatements.Add(new CodeMethodReturnStatement(new CodeArgumentReferenceExpression(String.Empty)));
            //p.Name = "StrategyUsed";
            //p.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            //reference = new CodeTypeReference(" string");
            //p.Type = reference;
            //// Description
            //comment = new CodeComment(
            //    "<summary>" + "Reference to the ontology" + "</summary>",
            //    true);
            //commentStatement = new CodeCommentStatement(comment);
            //p.Comments.Add(commentStatement);
            p.EndDirectives.Add(new CodeRegionDirective(CodeRegionMode.End,
                     "None"));
            //Class.Members.Add(p);

            #endregion

            #region ParametersIO

            if (!fr.LoadDomainClassFromInterfacesDllAndExtendIt)
            {
                m = new CodeMemberField("ParametersIO", "_parametersIO");
                m.StartDirectives.Add(new CodeRegionDirective(CodeRegionMode.Start, "Private field for properties"));
                m.EndDirectives.Add(new CodeRegionDirective(CodeRegionMode.End, String.Empty));
                Class.Members.Add(m);
            }

            #endregion

            #region IParameters members

            if (_IsParametersClass)
            {
                // IValuesReader Reader property
                p = new CodeMemberProperty();
                p.CustomAttributes.Add(new CodeAttributeDeclaration("ConfigurationItem"));
                p.StartDirectives.Add(new CodeRegionDirective(CodeRegionMode.Start, "IParameters members"));
                p.GetStatements.Add(new CodeSnippetExpression("return _parametersIO.Reader"));
                p.Name = "Reader";
                p.Attributes = MemberAttributes.Public | MemberAttributes.Final;
                reference = new CodeTypeReference("IValuesReader");
                p.Type = reference;
                //description
                comment = new CodeComment(
                    "<summary>The reader used to read parameters' definition and values</summary>",
                    true);
                commentStatement = new CodeCommentStatement(comment);
                p.Comments.Add(commentStatement);
                p.SetStatements.Add(new CodeSnippetExpression("_parametersIO.Reader = value"));
                Class.Members.Add(p);

                // IValuesWriter Writer property
                p = new CodeMemberProperty();
                p.CustomAttributes.Add(new CodeAttributeDeclaration("ConfigurationItem"));
                p.GetStatements.Add(new CodeSnippetExpression("return _parametersIO.Writer"));
                p.Name = "Writer";
                p.Attributes = MemberAttributes.Public | MemberAttributes.Final;
                reference = new CodeTypeReference("IValuesWriter");
                p.Type = reference;
                //description
                comment = new CodeComment(
                    "<summary>The writer used to write parameters' values</summary>",
                    true);
                commentStatement = new CodeCommentStatement(comment);
                p.Comments.Add(commentStatement);
                p.SetStatements.Add(new CodeSnippetExpression("_parametersIO.Writer = value"));
                Class.Members.Add(p);

                /* 11/10/2012 OLD Replaced by the extension method
                // LoadParameters Method
                CodeMemberMethod Method1 = new CodeMemberMethod();
                comment = new CodeComment("<summary>Load parameter values from the Reader</summary>", true);
                commentStatement = new CodeCommentStatement(comment);
                Method1.Comments.Add(commentStatement);
                Method1.Attributes = MemberAttributes.Final;
                Method1.Name = "LoadParameters";
                CodeParameterDeclarationExpression param1 =
                    new CodeParameterDeclarationExpression("System.String", "parametersKey");
                Method1.Attributes = MemberAttributes.Public;
                Method1.Parameters.Add(param1);
                Method1.Statements.Add(new CodeSnippetExpression("_parametersIO.LoadParameters(parametersKey)"));
                Class.Members.Add(Method1);
                 */

                // SaveParameters Method
                CodeMemberMethod Method1 = new CodeMemberMethod();
                Method1.ReturnType = new CodeTypeReference("System.String");
                comment = new CodeComment("<summary>Save parameter values into the Writer</summary>", true);
                commentStatement = new CodeCommentStatement(comment);
                Method1.Comments.Add(commentStatement);
                Method1.Attributes = MemberAttributes.Final;
                Method1.Name = "SaveParameters";
                CodeParameterDeclarationExpression param1 = new CodeParameterDeclarationExpression("System.String", "parametersKey");
                Method1.Attributes = MemberAttributes.Public;
                Method1.Parameters.Add(param1);
                Method1.Statements.Add(new CodeSnippetExpression("return _parametersIO.SaveParameters(parametersKey)"));
                Class.Members.Add(Method1);

                // SetParameters Method
                Method1 = new CodeMemberMethod();
                comment = new CodeComment("<summary>Set parameter values at run time. It might be a sub-set of parameters.</summary>", true);
                commentStatement = new CodeCommentStatement(comment);
                Method1.Comments.Add(commentStatement);
                Method1.Attributes = MemberAttributes.Final;
                Method1.Name = "SetParameters";
                param1 = new CodeParameterDeclarationExpression("IEnumerable<VarInfo>", "parametersVarInfoSource");
                Method1.Attributes = MemberAttributes.Public;
                Method1.Parameters.Add(param1);
                Method1.Statements.Add(new CodeSnippetExpression("_parametersIO.SetParameters(parametersVarInfoSource)"));
                Method1.EndDirectives.Add(new CodeRegionDirective(CodeRegionMode.End, String.Empty));
                Class.Members.Add(Method1);

                /*davidefuma aggiunta dichiarazione evento ParameterClassPropertyValueSet START*/
                if (fr.CreateParametersClass)
                {//event present only in parameter classes
                    
                    // ParameterClassPropertyValueSet event
                    CodeMemberEvent event1 = new CodeMemberEvent();
                    comment = new CodeComment("<summary>Event launched when one of the values of the parameters is set.</summary>", true);
                    commentStatement = new CodeCommentStatement(comment);
                    event1.Comments.Add(commentStatement);
                    event1.Name = "ParameterClassPropertyValueSet";
                    event1.Type = new CodeTypeReference("Action<string,object>");
                    event1.Attributes = MemberAttributes.Public;
                    Class.Members.Add(event1);
                }
                /*davidefuma aggiunta dichiarazione evento ParameterClassPropertyValueSet END*/

            }

            #endregion

            #region ICloneable implementation

            if (!fr.LoadDomainClassFromInterfacesDllAndExtendIt)
            {

                // Generate code to clone type
                CodeMemberMethod Method2 = new CodeMemberMethod();
                comment = new CodeComment("<summary>Implement ICloneable.Clone()</summary>", true);
                commentStatement = new CodeCommentStatement(comment);
                Method2.Comments.Add(commentStatement);
                //Method2.Attributes = MemberAttributes.Final | MemberAttributes.Public;
                Method2.Attributes = MemberAttributes.Public;
                CodeTypeReference codref = new CodeTypeReference("Object");
                Method2.ReturnType = codref;
                Method2.Name = "Clone";
                Method2.StartDirectives.Add((new CodeRegionDirective(CodeRegionMode.Start,
                                                                     "Clone")));
                Method2.Statements.Add(new CodeCommentStatement("Shallow copy by default"));
                string _MemberwiseStatement;
                _MemberwiseStatement = "IDomainClass myclass = (IDomainClass) this.MemberwiseClone()";
                Method2.Statements.Add(new CodeSnippetExpression(_MemberwiseStatement));

                /* DCC - MPE - refactoring - begin */
                /* old - begin */
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    string _literalCode;
                //    VarInfoAttributes v = new VarInfoAttributes(dt.Rows[i]);
                //    string _type = v.type;

                //    // Select type for specific deep copy
                //    if (_type == "List<double>")
                //    {
                //        Method2.Statements.Add(new CodeCommentStatement("Deep copying List<double>"));
                //        _MemberwiseStatement = "myclass." + v.name + " = new List<double>()";
                //        Method2.Statements.Add(new CodeSnippetExpression(_MemberwiseStatement));
                //        _literalCode = " foreach (double d in this._" + v.name + ")";
                //        Method2.Statements.Add(new CodeSnippetStatement(_literalCode));
                //        Method2.Statements.Add(new CodeSnippetStatement("{"));
                //        _MemberwiseStatement = "myclass." + v.name + ".Add(d)";
                //        Method2.Statements.Add(new CodeSnippetExpression(_MemberwiseStatement));
                //        Method2.Statements.Add(new CodeSnippetStatement("}"));
                //    }
                //    else if (_type == "List<int>")
                //    {
                //        Method2.Statements.Add(new CodeCommentStatement("Deep copying List<int>"));
                //        _MemberwiseStatement = "myclass." + v.name + " = new List<int>()";
                //        Method2.Statements.Add(new CodeSnippetExpression(_MemberwiseStatement));
                //        _literalCode = " foreach (int d in this._" + v.name + ")";
                //        Method2.Statements.Add(new CodeSnippetStatement(_literalCode));
                //        Method2.Statements.Add(new CodeSnippetStatement("{"));
                //        _MemberwiseStatement = "myclass." + v.name + ".Add(d)";
                //        Method2.Statements.Add(new CodeSnippetExpression(_MemberwiseStatement));
                //        Method2.Statements.Add(new CodeSnippetStatement("}"));
                //    }
                //    else if (_type == "List<bool>")
                //    {
                //        Method2.Statements.Add(new CodeCommentStatement("Deep copying List<int>"));
                //        _MemberwiseStatement = "myclass." + v.name + " = new List<int>()";
                //        Method2.Statements.Add(new CodeSnippetExpression(_MemberwiseStatement));
                //        _literalCode = " foreach (bool d in this._" + v.name + ")";
                //        Method2.Statements.Add(new CodeSnippetStatement(_literalCode));
                //        Method2.Statements.Add(new CodeSnippetStatement("{"));
                //        _MemberwiseStatement = "myclass." + v.name + ".Add(d)";
                //        Method2.Statements.Add(new CodeSnippetExpression(_MemberwiseStatement));
                //        Method2.Statements.Add(new CodeSnippetStatement("}"));
                //    }
                //    else if (_type == "List<string>")
                //    {
                //        Method2.Statements.Add(new CodeCommentStatement("Deep copying List<int>"));
                //        _MemberwiseStatement = "myclass." + v.name + " = new List<int>()";
                //        Method2.Statements.Add(new CodeSnippetExpression(_MemberwiseStatement));
                //        _literalCode = " foreach (string d in this._" + v.name + ")";
                //        Method2.Statements.Add(new CodeSnippetStatement(_literalCode));
                //        Method2.Statements.Add(new CodeSnippetStatement("{"));
                //        _MemberwiseStatement = "myclass." + v.name + ".Add(d)";
                //        Method2.Statements.Add(new CodeSnippetExpression(_MemberwiseStatement));
                //        Method2.Statements.Add(new CodeSnippetStatement("}"));
                //    }
                //    // Dictionary<string, double>
                //    else if (_type == "Dictionary<string,double>")
                //    {
                //        Method2.Statements.Add(new CodeCommentStatement("Deep copying Dictionary<string, double>"));
                //        _MemberwiseStatement = "myclass." + v.name + " = new Dictionary<string, double>()";
                //        Method2.Statements.Add(new CodeSnippetExpression(_MemberwiseStatement));
                //        _literalCode = " foreach (KeyValuePair<string, double> kvp in this._" + v.name + ")";
                //        Method2.Statements.Add(new CodeSnippetStatement(_literalCode));
                //        Method2.Statements.Add(new CodeSnippetStatement("{"));
                //        _MemberwiseStatement = "myclass." + v.name + ".Add(kvp.Key, kvp.Value)";
                //        Method2.Statements.Add(new CodeSnippetExpression(_MemberwiseStatement));
                //        Method2.Statements.Add(new CodeSnippetStatement("}"));
                //    }
                //    // Dictionary<string, string>
                //    else if (_type == "Dictionary<string,string>")
                //    {
                //        Method2.Statements.Add(new CodeCommentStatement("Deep copying Dictionary<string, string>"));
                //        _MemberwiseStatement = "myclass." + v.name + " = new Dictionary<string, string>()";
                //        Method2.Statements.Add(new CodeSnippetExpression(_MemberwiseStatement));
                //        _literalCode = " foreach (KeyValuePair<string, string> kvp in this._" + v.name + ")";
                //        Method2.Statements.Add(new CodeSnippetStatement(_literalCode));
                //        Method2.Statements.Add(new CodeSnippetStatement("{"));
                //        _MemberwiseStatement = "myclass." + v.name + ".Add(kvp.Key, kvp.Value)";
                //        Method2.Statements.Add(new CodeSnippetExpression(_MemberwiseStatement));
                //        Method2.Statements.Add(new CodeSnippetStatement("}"));
                //    }
                //    // Dictionary<int, string>
                //    else if (_type == "Dictionary<int,string>")
                //    {
                //        Method2.Statements.Add(new CodeCommentStatement("Deep copying Dictionary<int, string>"));
                //        _MemberwiseStatement = "myclass." + v.name + " = new Dictionary<int, string>()";
                //        Method2.Statements.Add(new CodeSnippetExpression(_MemberwiseStatement));
                //        _literalCode = " foreach (KeyValuePair<int, string> kvp in this._" + v.name + ")";
                //        Method2.Statements.Add(new CodeSnippetStatement(_literalCode));
                //        Method2.Statements.Add(new CodeSnippetStatement("{"));
                //        _MemberwiseStatement = "myclass." + v.name + ".Add(kvp.Key, kvp.Value)";
                //        Method2.Statements.Add(new CodeSnippetExpression(_MemberwiseStatement));
                //        Method2.Statements.Add(new CodeSnippetStatement("}"));
                //    }

                //    else if ((_type.Contains("Dictionary<") && !_type.Contains("string,double>"))
                //        && (_type.Contains("Dictionary<") && !_type.Contains("int,string>"))
                //        && (_type.Contains("Dictionary<") && !_type.Contains("string,string>")))
                //    {
                //        throw new Exception("Type " + _type + " not allowed!");
                //    }
                //    // One dimension arrays
                //    else if ((_type.Contains("double[")
                //        || _type.Contains("string[")
                //        || _type.Contains("int[")
                //        || _type.Contains("bool["))
                //        &&
                //        !_type.Contains(","))
                //    {
                //        // Cloning arrays double/int/bool
                //        Method2.Statements.Add(new CodeCommentStatement("Deep copying array " + v.type));
                //        int begin = _type.IndexOf("[") + 1;
                //        int end = _type.IndexOf("]");
                //        int length = end - begin;
                //        int count = int.Parse(_type.Substring(begin, length));
                //        _MemberwiseStatement = "myclass." + v.name + " = new " + _type;
                //        Method2.Statements.Add(new CodeSnippetExpression(_MemberwiseStatement));
                //        _literalCode = " for (int j = 0; j < " + count + "; j++)";
                //        Method2.Statements.Add(new CodeSnippetStatement(_literalCode));
                //        Method2.Statements.Add(new CodeSnippetStatement("{"));
                //        _MemberwiseStatement = "myclass." + v.name + "[j] = this._" + v.name + "[j]";
                //        Method2.Statements.Add(new CodeSnippetExpression(_MemberwiseStatement));
                //        Method2.Statements.Add(new CodeSnippetStatement("}"));
                //    }
                //    // Two dimension array of doubles
                //    else if (_type.Contains("double[")
                //        && _type.Contains(","))
                //    {
                //        // Cloning arrays double[,]
                //        Method2.Statements.Add(new CodeCommentStatement("Deep copying array " + v.type));
                //        int begin = _type.IndexOf("[") + 1;
                //        int end1 = _type.IndexOf(",");
                //        int end = _type.IndexOf("]");
                //        int length = end1 - begin;
                //        int length1 = end - (end1 + 1);
                //        int count = int.Parse(_type.Substring(begin, length));
                //        int count1 = int.Parse(_type.Substring((end1 + 1), length1));
                //        _MemberwiseStatement = "myclass." + v.name + " = new " + _type;
                //        Method2.Statements.Add(new CodeSnippetExpression(_MemberwiseStatement));
                //        // frist dimension loop
                //        _literalCode = " for (int j = 0; j < " + count + "; j++)";
                //        Method2.Statements.Add(new CodeSnippetStatement(_literalCode));
                //        Method2.Statements.Add(new CodeSnippetStatement("{"));
                //        // second dimension loop
                //        _literalCode = " for (int i = 0; i < " + count1 + "; i++)";
                //        Method2.Statements.Add(new CodeSnippetStatement(_literalCode));
                //        Method2.Statements.Add(new CodeSnippetStatement("{"));
                //        _MemberwiseStatement = "myclass." + v.name + "[j,i] = this._" + v.name + "[j,i]";
                //        Method2.Statements.Add(new CodeSnippetExpression(_MemberwiseStatement));
                //        Method2.Statements.Add(new CodeSnippetStatement("}"));
                //        Method2.Statements.Add(new CodeSnippetStatement("}"));
                //    }
                //    // List of a user defined type which must implement ICloneable - deep copy
                //    else if (_type.Contains("List<") &&
                //        !_type.Contains("double") &&
                //        !_type.Contains("int") &&
                //        !_type.Contains("bool") &&
                //        !_type.Contains("string"))
                //    {
                //        Method2.Statements.Add(new CodeCommentStatement("Deep copying " + v.type));
                //        _MemberwiseStatement = "myclass." + v.name + " = new " + v.type + "()";
                //        Method2.Statements.Add(new CodeSnippetExpression(_MemberwiseStatement));
                //        int _begin = v.type.IndexOf("<");
                //        string _T = v.type.Substring(_begin + 1, v.type.Length - _begin - 2);
                //        _literalCode = " foreach (" + _T + " d in this._" + v.name + ")";
                //        Method2.Statements.Add(new CodeSnippetStatement(_literalCode));
                //        Method2.Statements.Add(new CodeSnippetStatement("{"));
                //        // try
                //        _literalCode = "try {";
                //        Method2.Statements.Add(new CodeSnippetStatement(_literalCode));
                //        _MemberwiseStatement = "myclass." + v.name + ".Add((" + _T + ")d.Clone())";
                //        Method2.Statements.Add(new CodeSnippetExpression(_MemberwiseStatement));
                //        Method2.Statements.Add(new CodeSnippetStatement("}"));
                //        // catch
                //        _literalCode = "catch (Exception err) {";
                //        Method2.Statements.Add(new CodeSnippetStatement(_literalCode));
                //        _literalCode = "string msg = " + (char)34 + "The type " + _T + " does not implement ICloneable" + (char)34 + ";";
                //        Method2.Statements.Add(new CodeSnippetStatement(_literalCode));
                //        Method2.Statements.Add(new CodeSnippetExpression("throw new Exception(msg + err)"));
                //        Method2.Statements.Add(new CodeSnippetStatement("}"));

                //        Method2.Statements.Add(new CodeSnippetStatement("}"));
                //    }
                //    else if (_type == "double"
                //        || _type == "int"
                //        || _type == "string"
                //        || _type == "bool")
                //    {
                //        // Do nothing - copied already be default
                //    }
                //    else
                //    {
                //        // Cloning contained type which implements ICloneable
                //        Method2.Statements.Add(new CodeCommentStatement("Deep copying instance of " + _type));
                //        Method2.Statements.Add(new CodeCommentStatement("Assume " + _type + "implements ICloneable"));
                //        Method2.Statements.Add(new CodeCommentStatement("An exception is thrown if ICloneable is not implemented"));
                //        // try
                //        _literalCode = "try {";
                //        Method2.Statements.Add(new CodeSnippetStatement(_literalCode));
                //        _MemberwiseStatement = "myclass." + v.name + " = new " + _type + "()";
                //        Method2.Statements.Add(new CodeSnippetExpression(_MemberwiseStatement));
                //        _MemberwiseStatement = "myclass." + v.name + " = (" + _type + ") this._" + v.name + ".Clone()";
                //        Method2.Statements.Add(new CodeSnippetExpression(_MemberwiseStatement));
                //        Method2.Statements.Add(new CodeSnippetStatement("}"));
                //        // catch
                //        _literalCode = "catch (Exception err) {";
                //        Method2.Statements.Add(new CodeSnippetStatement(_literalCode));
                //        _literalCode = "string msg = " + (char)34 + "The type " + _type + " does not implement ICloneable" + (char)34 + ";";
                //        Method2.Statements.Add(new CodeSnippetStatement(_literalCode));
                //        Method2.Statements.Add(new CodeSnippetExpression("throw new Exception(msg + err)"));
                //        Method2.Statements.Add(new CodeSnippetStatement("}"));
                //    }
                //}
                /* old - end */
                Method2.Statements.Add(new CodeSnippetExpression("_parametersIO.PopulateClonedCopy(myclass)"));
                /* DCC - MPE - refactoring - end */
                Method2.Statements.Add(new CodeSnippetExpression("return myclass"));

                Method2.EndDirectives.Add(new CodeRegionDirective(CodeRegionMode.End, String.Empty));

                Class.Members.Add(Method2);
            }
            if (strategyRow != null)
            {
                dt.Rows.Remove(strategyRow);
            }

            #endregion

            return compileUnit;
        }
    }
}
