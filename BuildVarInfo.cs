using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using CRA.ModelLayer.Core;
//using CRA.Core.Preconditions;

namespace CRA.ModelLayer.DCC
{
    internal class BuildVarInfo
    {
        
        
        internal CodeCompileUnit WriteVarInfo(Form1 fr,
                                              DataSet d,
                                              List<CodeCommentStatement> commentsToClass)
        {
            DataTable dt = d.Tables["VarInfoVariables"];
            
            // Create a new CodeCompileUnit to contain the program graph
            CodeCompileUnit compileUnit2 = new CodeCompileUnit();

            // Declare  namespace chosen 
            string _nameSpace = fr.txtNameSpace.Text;
            CodeNamespace DomainClassVF = new CodeNamespace(fr.txtNameSpace.Text);
            // Add the new namespace to the compile unit.
            compileUnit2.Namespaces.Add( DomainClassVF );
            // Add the new namespace import for the System namespace.
            DomainClassVF.Imports.Add( new CodeNamespaceImport("System") );
            DomainClassVF.Imports.Add(new CodeNamespaceImport("CRA.ModelLayer.Core"));

            //comment defined in the value class code
            DomainClassVF.Comments.Add(commentsToClass[0]);
            DomainClassVF.Comments.Add(commentsToClass[1]); // web portal
            DomainClassVF.Comments.Add(commentsToClass[2]); // DCC
            if (commentsToClass.Count == 8)
            {
                DomainClassVF.Comments.Add(commentsToClass[0]);
                DomainClassVF.Comments.Add(commentsToClass[4]);  // Author
                DomainClassVF.Comments.Add(commentsToClass[5]);
                DomainClassVF.Comments.Add(commentsToClass[6]);
                DomainClassVF.Comments.Add(commentsToClass[7]);
            }
            DomainClassVF.Comments.Add(commentsToClass[0]);
            DomainClassVF.Comments.Add(commentsToClass[3]);  // Date
            DomainClassVF.Comments.Add(commentsToClass[0]);
		    
            // Declare a new type 
            CodeTypeDeclaration ClassVF  = new CodeTypeDeclaration(fr.txtClassName.Text + "VarInfo");
            ClassVF.Attributes = MemberAttributes.Public;
            ClassVF.BaseTypes.Add("IVarInfoClass");
            // Add the new type to the namespace's type collection.
            DomainClassVF.Types.Add(ClassVF);
            // class description
            CodeComment commentClass = new CodeComment(
                "<summary>" + fr.txtClassName.Text + "VarInfoClasses contain the attributes for each variable in the domain class RainData." + 
                " Attributes are valorized via the static constructor. The data-type VarInfo causes" +
                "  a dependency to the assembly CRA.Core.Preconditions.dll</summary>",
                true );
            CodeCommentStatement classCommentStatement = new CodeCommentStatement( commentClass );
            ClassVF.Comments.Add(classCommentStatement);
	
            // method set varInfo values
            CodeMemberMethod Method1 = new CodeMemberMethod();
            commentClass = new CodeComment("<summary>Set VarInfo values</summary>", true);
            classCommentStatement = new CodeCommentStatement(commentClass);
            Method1.Comments.Add(classCommentStatement);
            Method1.Attributes = MemberAttributes.Static;
            Method1.Name = "DescribeVariables";
            Method1.StartDirectives.Add((new CodeRegionDirective(CodeRegionMode.Start,
                                                                 "VarInfo values")));
            Method1.EndDirectives.Add(new CodeRegionDirective(CodeRegionMode.End,
                                                              String.Empty));
            ClassVF.Members.Add(Method1);

            // Defines constructor
            CodeTypeConstructor constructor = new CodeTypeConstructor();
            commentClass = new CodeComment("<summary>Constructor</summary>", true);
            classCommentStatement = new CodeCommentStatement(commentClass);
            constructor.Comments.Add(classCommentStatement);
            constructor.Attributes = MemberAttributes.Public | MemberAttributes.Static;
            // constructor methods
            CodeTypeReferenceExpression typeRef = new CodeTypeReferenceExpression(fr.txtClassName.Text + "VarInfo");
            CodeMethodInvokeExpression cd = new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(typeRef, Method1.Name)); //CodeMethodReferenceExpression(),
            constructor.Statements.Add(cd);
            // Add the constructor
            ClassVF.Members.Add(constructor);

            CodeMemberProperty p;
            CodeMemberField m2 = new CodeMemberField("VarInfo", "_variable");
            
            //string _type;
            //IVarInfoClass properties
            //description
            p = new CodeMemberProperty();
            p.StartDirectives.Add(new CodeRegionDirective(CodeRegionMode.Start,
                                                          "IVarInfoClass members"));
            p.GetStatements.Add(new CodeMethodReturnStatement(new CodeArgumentReferenceExpression((char)34 + fr.txtDescription.Text + (char)34)));
            p.Name = "Description";
            p.Attributes = MemberAttributes.Public;
            CodeTypeReference reference = new CodeTypeReference(" string");
            p.Type = reference;
            CodeComment comment = new CodeComment(
                "<summary>" + "Domain Class description" + "</summary>",
                true);
            CodeCommentStatement commentStatement = new CodeCommentStatement(comment);
            p.Comments.Add(commentStatement);
            ClassVF.Members.Add(p);

            //URL
            p = new CodeMemberProperty();
            p.GetStatements.Add(new CodeMethodReturnStatement(new CodeArgumentReferenceExpression((char)34 + fr.txtURL.Text + (char)34)));
            p.Name = "URL";
            p.Attributes = MemberAttributes.Public|MemberAttributes.Final;
            reference = new CodeTypeReference(" string");
            p.Type = reference;
            comment = new CodeComment(
                "<summary>" + "Reference to the ontology" + "</summary>",
                true);
            commentStatement = new CodeCommentStatement(comment);
            p.Comments.Add(commentStatement);
            ClassVF.Members.Add(p);

            //DomainClassOfReference
            p = new CodeMemberProperty();
            p.GetStatements.Add(new CodeMethodReturnStatement(new CodeArgumentReferenceExpression((char)34 + fr.txtClassName.Text + (char)34)));
            p.Name = "DomainClassOfReference";
            p.Attributes = MemberAttributes.Public|MemberAttributes.Final;
            reference = new CodeTypeReference(" string");
            p.Type = reference;
            //description
            comment = new CodeComment(
                "<summary>" + "Value domain class of reference" + "</summary>",
                true);
            commentStatement = new CodeCommentStatement(comment);
            p.Comments.Add(commentStatement);
            p.EndDirectives.Add(new CodeRegionDirective(CodeRegionMode.End,
                                                        String.Empty));
            ClassVF.Members.Add(p);
		    
            //VarInfo variables
            string _reference2 = "_";
            //int _num = dt.Rows.Count - 1;
            

            for (int i=0; i<dt.Rows.Count; i++)
            {
                VarInfoAttributes v = new VarInfoAttributes(dt.Rows[i]);
                string type = v.type;
                int size;
                if (v.name.Equals("Strategy")) continue;
                VarInfoValueTypes varInfoValueType = VarInfoValueTypes.GetVarInfoValueType(v.type, out size);
                // 7/6/2012 - DFa - took off constraint
                //_type = v.type.ToLower();
                //if (!_type.Contains("dictionary") && !_type.Contains("list"))
                //{
                //    if (_type.Contains("double") || _type.Contains("int"))
                //    {
                        switch (fr.cmbLanguage.Text)
                        {
                            case "C#":
                                _reference2 = "_" + v.name + " = new VarInfo()";
                                break;
                            case "VB":
                                _reference2 = "_" + v.name; //TODO
                                break;
                        }
                        m2 = new CodeMemberField("VarInfo", _reference2);
                        //add region for private fields (start to first, end to last)
                        if (i == 0)
                        {
                            m2.StartDirectives.Add(new CodeRegionDirective(CodeRegionMode.Start,
                                                                           "Private fields"));
                        }
                        //
                        m2.Attributes = MemberAttributes.Private;
                        m2.Attributes = MemberAttributes.Static;
                        ClassVF.Members.Add(m2);
                        //properties
                        p = new CodeMemberProperty();
                        //add region for private fields (start to first, end to last)
                        if (i == 0)
                        {
                            p.StartDirectives.Add(new CodeRegionDirective(CodeRegionMode.Start,
                                                                          "Public properties"));
                        }
                        //
                        p.GetStatements.Add(
                            new CodeMethodReturnStatement(new CodeArgumentReferenceExpression(" _" + v.name)));
                        p.Name = v.name;
                        p.Attributes = MemberAttributes.Static | MemberAttributes.Public;
                        reference = new CodeTypeReference(" VarInfo");
                        p.Type = reference;
                        //description
                        comment = new CodeComment(
                            "<summary>" + v.description + "</summary>",
                            true);
                        commentStatement = new CodeCommentStatement(comment);
                        p.Comments.Add(commentStatement);
                        ClassVF.Members.Add(p);

                        //VarInfo values
                        comment = new CodeComment(
                            "  ",
                            false);
                        commentStatement = new CodeCommentStatement(comment);
                        Method1.Statements.Add(commentStatement);
                        CodeAssignStatement as1 =
                            new CodeAssignStatement(new CodeVariableReferenceExpression("_" + v.name + ".Name"),
                                                    new CodePrimitiveExpression(v.name));
                        Method1.Statements.Add(as1);
                        CodeAssignStatement as2 =
                            new CodeAssignStatement(new CodeVariableReferenceExpression("_" + v.name + ".Description"),
                                                    new CodePrimitiveExpression(v.description));
                        Method1.Statements.Add(as2);
                        CodeAssignStatement as3 =
                            new CodeAssignStatement(new CodeVariableReferenceExpression("_" + v.name + ".MaxValue"),
                                                    new CodePrimitiveExpression(v.maxValue));
                        Method1.Statements.Add(as3);
                        CodeAssignStatement as4 =
                            new CodeAssignStatement(new CodeVariableReferenceExpression("_" + v.name + ".MinValue"),
                                                    new CodePrimitiveExpression(v.minValue));
                        Method1.Statements.Add(as4);
                        CodeAssignStatement as5 =
                            new CodeAssignStatement(
                                new CodeVariableReferenceExpression("_" + v.name + ".DefaultValue"),
                                new CodePrimitiveExpression(v.defaultValue));
                        Method1.Statements.Add(as5);
                        CodeAssignStatement as6 =
                            new CodeAssignStatement(new CodeVariableReferenceExpression("_" + v.name + ".Units"),
                                                    new CodePrimitiveExpression(v.units));
                        Method1.Statements.Add(as6);
                        CodeAssignStatement as7 =
                            new CodeAssignStatement(new CodeVariableReferenceExpression("_" + v.name + ".URL"),
                                                    new CodePrimitiveExpression(v.URL));
                        Method1.Statements.Add(as7);
                        // 7/6/2012 - DFa - added VarInfoValueTypes property
                        CodeAssignStatement as8 =
                            new CodeAssignStatement(new CodeVariableReferenceExpression("_" + v.name + ".ValueType"),
                                                    //new CodePrimitiveExpression("VarInfoValueTypes." + varInfoValueType.Name)
                                                    new CodeVariableReferenceExpression("VarInfoValueTypes.GetInstanceForName(" + (char)34 + varInfoValueType.Name + (char)34 + ")")
                                                    );
                        Method1.Statements.Add(as8);
                        // 7/6/2012 - DFa - took off constraint
                //    }
                //}
            }
            //End region private fields
            m2.EndDirectives.Add(new CodeRegionDirective(CodeRegionMode.End, String.Empty));
            //End region public VarInfo
            p.EndDirectives.Add(new CodeRegionDirective(CodeRegionMode.End, String.Empty));
            
            // TODO: add code to make read only from static properties VarInfo of class to be extended
            if (fr.LoadDomainClassFromInterfacesDllAndExtendIt)
            {
                try
                {
                    int j = 0;
                    string domainClass = fr.lblDomainClassToBeExtended.Text;
                    string fname = fr.lblHiddenAssemblyName.Text;
                    Assembly a = Assembly.LoadFile(fname);
                    Type test = a.GetType(domainClass); //Loads specific type
                    MemberInfo[] minf = test.GetProperties();
                    foreach (PropertyInfo p_info in minf)
                    {
                        // Marcello queste cose non si fanno! Mai!!
                        //if (p_info.PropertyType.ToString() == "CRA.Core.Preconditions.VarInfo")
                        if (typeof(VarInfo).IsAssignableFrom(p_info.PropertyType))
                        {
                            string _literalCode = domainClass + "." + p_info.Name;

                            //properties
                            p = new CodeMemberProperty();
                            //add region for private fields (start to first, end to last)
                            if (j == 0)
                            {
                                p.StartDirectives.Add(new CodeRegionDirective(CodeRegionMode.Start,
                                                                              "Public properties (read only from extended class)"));
                            }
                            //
                            p.GetStatements.Add(new CodeMethodReturnStatement(new CodeArgumentReferenceExpression(_literalCode)));
                            p.Name = p_info.Name;
                            p.Attributes = MemberAttributes.Static | MemberAttributes.Public;
                            reference = new CodeTypeReference(" VarInfo");
                            p.Type = reference;
                            //description
                            comment = new CodeComment(
                                "<summary>" + p_info.Name + " from extended class " + domainClass + "</summary>",
                                true);
                            commentStatement = new CodeCommentStatement(comment);
                            p.Comments.Add(commentStatement);
                            j++;
                            ClassVF.Members.Add(p);
                        }
                    }
                    p.EndDirectives.Add(new CodeRegionDirective(CodeRegionMode.End, "End extnded VariNfo"));
                }
                catch
                {
                    throw new Exception("Error while reading VarInfo static properties;\t\nRead statements of VarInfo in class to be extended might be missing!");
                }
            }
            
            //Compile unit completed
            return compileUnit2;
        }
    }
}
