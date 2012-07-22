// SharpDevelop samples
// Copyright (c) 2006, AlphaSierraPapa
// All rights reserved.
//
// Redistribution and use in source and binary forms, with or without modification, are
// permitted provided that the following conditions are met:
//
// - Redistributions of source code must retain the above copyright notice, this list
//   of conditions and the following disclaimer.
//
// - Redistributions in binary form must reproduce the above copyright notice, this list
//   of conditions and the following disclaimer in the documentation and/or other materials
//   provided with the distribution.
//
// - Neither the name of the SharpDevelop team nor the names of its contributors may be used to
//   endorse or promote products derived from this software without specific prior written
//   permission.
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS &AS IS& AND ANY EXPRESS
// OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY
// AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR
// CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
// DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
// DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER
// IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT
// OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Configuration;
using System.Collections;
using System.Xml;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;
using System.IO;

namespace XmlPad
{
	public partial class XmlEditor
	{
		const string _PropertySavePath = "XmlEditor.TextEditorControl.{0}";
		const string SharpPadFileFilter = "Xml Files (*.xml)|*.xml|All Files (*.*)|*.*";

		[STAThread]
		public static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new XmlEditor());
		}

		private string _xnOriginalXml;
		public string OriginalXml
		{
			get { return _xnOriginalXml; }
			set { _xnOriginalXml = value; }
		}

		public string TextContent { get; set; }

		bool _isEditPad = false;
		public bool IsEditPad
		{
			get { return _isEditPad; }
			set { _isEditPad = value; }
		}

		/// <summary>Returns the currently displayed editor, or null if none are open</summary>
		private TextEditorControl ActiveEditor
		{
			get
			{
				//if (fileTabs.TabPages.Count == 0) return null;
				//return fileTabs.SelectedTab.Controls.OfType<TextEditorControl>().FirstOrDefault();
				return this.textEditorControl;
			}
		}

		/// <summary>This variable holds the settings (whether to show line numbers, 
		/// etc.) that all editor controls share.</summary>
		ITextEditorProperties _editorSettings;

		public XmlEditor()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();

			InitForm();
		}

		/// <summary>
		/// Replaces the entire text of the xml view with the xml in the
		/// specified. The xml will be formatted.
		/// </summary>
		public void FormatXml(string xml)
		{
			string formattedXml = IndentedFormat(SimpleFormat(IndentedFormat(xml)));
			ActiveEditor.Document.Replace(0, ActiveEditor.Document.TextLength, formattedXml);
			UpdateFolding();
		}

		/// <summary>
		/// Forces the editor to update its folds.
		/// </summary>
		void UpdateFolding()
		{
			ActiveEditor.Document.FoldingManager.UpdateFoldings(String.Empty, null);
			ActiveEditor.ActiveTextAreaControl.TextArea.Refresh();
		}

		/// <summary>
		/// Returns a formatted xml string using a simple formatting algorithm.
		/// </summary>
		static string SimpleFormat(string xml)
		{
			return xml.Replace("><", ">\r\n<");
		}

		/// <summary>
		/// Returns a pretty print version of the given xml.
		/// </summary>
		/// <param name="xml">Xml string to pretty print.</param>
		/// <returns>A pretty print version of the specified xml.  If the
		/// string is not well formed xml the original string is returned.
		/// </returns>
		string IndentedFormat(string xml)
		{
			string indentedText = String.Empty;

			try
			{
				XmlTextReader reader = new XmlTextReader(new StringReader(xml));
				reader.WhitespaceHandling = WhitespaceHandling.None;

				StringWriter indentedXmlWriter = new StringWriter();
				XmlTextWriter writer = CreateXmlTextWriter(indentedXmlWriter);
				writer.WriteNode(reader, false);
				writer.Flush();

				indentedText = indentedXmlWriter.ToString();
			}
			catch (Exception)
			{
				indentedText = xml;
			}

			return indentedText;
		}

		XmlTextWriter CreateXmlTextWriter(TextWriter textWriter)
		{
			XmlTextWriter writer = new XmlTextWriter(textWriter);
			if (ActiveEditor.TextEditorProperties.ConvertTabsToSpaces)
			{
				writer.Indentation = ActiveEditor.TextEditorProperties.IndentationSize;
				writer.IndentChar = ' ';
			}
			else
			{
				writer.Indentation = 1;
				writer.IndentChar = '\t';
			}
			writer.Formatting = Formatting.Indented;
			return writer;
		}

		private void InitForm()
		{
			IDictionary syntaxModes = ConfigurationManager.GetSection("SyntaxModes") as IDictionary;
			if (syntaxModes != null)
			{
				foreach (string key in syntaxModes.Keys)
				{
					ToolStripMenuItem miNewMode = new ToolStripMenuItem();
					miNewMode.Name = "mi" + key;
					//this.miNewMode.Size = new System.Drawing.Size(174, 22);
					miNewMode.Text = key;
					miNewMode.Tag = syntaxModes[key];
					miNewMode.Click += new System.EventHandler(
						delegate(object sender, EventArgs e)
						{
							ToolStripMenuItem miThis = sender as ToolStripMenuItem;
							if (miThis != null && miThis.Tag != null)
							{
								ToolStripDropDownMenu owner = miThis.Owner as ToolStripDropDownMenu;
								if (owner != null && owner.Items.Count > 0)
								{
									foreach (ToolStripMenuItem mi in owner.Items)
									{
										mi.Checked = false;
									}
									miThis.Checked = true;
								}
								if (ActiveEditor != null)
								{
									ActiveEditor.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy(miThis.Tag as string);
								}
							}
						});
					if (key == "XML")
					{
						miNewMode.Checked = true;
						ActiveEditor.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("XML");
					}
					this.miViewMode.DropDownItems.Add(miNewMode);
				}
			}

			if (_editorSettings == null)
			{
				_editorSettings = ActiveEditor.TextEditorProperties;
				OnSettingsChanged();
			}
			else
				ActiveEditor.TextEditorProperties = _editorSettings;

			if (!(ActiveEditor.Document.FoldingManager.FoldingStrategy is XmlFoldingStrategy))
			{
				ActiveEditor.Document.FoldingManager.FoldingStrategy = new XmlFoldingStrategy();
			}
			UpdateFolding();
		}

		#region 菜单事件
		void miExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		void miOpen_Click(object sender, EventArgs e)
		{
			TextEditorControl editor = ActiveEditor;
			if (editor != null)
			{
				using (OpenFileDialog dialog = new OpenFileDialog())
				{
					dialog.Filter = SharpPadFileFilter;
					dialog.FilterIndex = 0;
					if (DialogResult.OK == dialog.ShowDialog())
					{
						editor.LoadFile(dialog.FileName);
						CheckCurrentViewMode(editor.Document.HighlightingStrategy.Name);
						if (Path.GetExtension(dialog.FileName).ToLower() == ".xml")
						{
							if (!(ActiveEditor.Document.FoldingManager.FoldingStrategy is XmlFoldingStrategy))
							{
								ActiveEditor.Document.FoldingManager.FoldingStrategy = new XmlFoldingStrategy();
							}
							UpdateFolding();
						}
					}
				}
			}
		}

		private void CheckCurrentViewMode(string modeName)
		{
			TextEditorControl editor = ActiveEditor;
			if (editor != null)
			{
				if (editor.Document.HighlightingStrategy != null && this.miViewMode.DropDownItems.Count > 0)
				{
					foreach (ToolStripMenuItem mi in this.miViewMode.DropDownItems)
					{
						if (mi.Tag != null && mi.Tag.ToString().Equals(modeName, StringComparison.OrdinalIgnoreCase))
						{
							mi.Checked = true;
						}
						else
						{
							mi.Checked = false;
						}
					}
				}
			}
		}

		void miSaveAs_Click(object sender, System.EventArgs e)
		{
			SaveAs();
		}

		void SaveAs()
		{
			TextEditorControl editor = ActiveEditor;
			if (editor != null)
			{
				using (SaveFileDialog dialog = new SaveFileDialog())
				{
					dialog.Filter = SharpPadFileFilter;
					dialog.FilterIndex = 0;
					if (DialogResult.OK == dialog.ShowDialog())
					{
						editor.SaveFile(dialog.FileName);
						editor.FileName = dialog.FileName;
					}
				}
			}
		}

		void miSave_Click(object sender, System.EventArgs e)
		{
			TextEditorControl editor = ActiveEditor;
			if (editor != null)
			{
				if (editor.FileName != null)
				{
					editor.SaveFile(editor.FileName);
				}
				else
				{
					SaveAs();
				}
			}
		}

		#endregion
		#region 按钮事件
		private void btnOK_Click(object sender, EventArgs e)
		{
			try
			{
				XmlDocument xdTmp = new XmlDocument();
				xdTmp.LoadXml(ActiveEditor.Document.TextContent);
				this.TextContent = xdTmp.InnerXml;

				this.DialogResult = DialogResult.OK;
				this.Close();
			}
			catch (System.Exception ex)
			{
				MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}
		#endregion

		private void miSplitWindow_Click(object sender, EventArgs e)
		{
			TextEditorControl editor = ActiveEditor;
			if (editor == null) return;
			editor.Split();
			OnSettingsChanged();
		}

		/// <summary>Show current settings on the Options menu</summary>
		/// <remarks>We don't have to sync settings between the editors because 
		/// they all share the same DefaultTextEditorProperties object.</remarks>
		private void OnSettingsChanged()
		{
			this.miSplitWindow.Checked = ActiveEditor.IsSplited;
			this.miShowSpacesTabs.Checked = _editorSettings.ShowSpaces;
			this.miShowEOLMarkers.Checked = _editorSettings.ShowEOLMarker;
			this.miShowInvalidLines.Checked = _editorSettings.ShowInvalidLines;
			this.miHLCurRow.Checked = _editorSettings.LineViewerStyle == LineViewerStyle.FullRow;
			this.miBracketMatchingStyle.Checked = _editorSettings.BracketMatchingStyle == BracketMatchingStyle.After;
			this.miEnableVirtualSpace.Checked = _editorSettings.AllowCaretBeyondEOL;
			this.miShowLineNumbers.Checked = _editorSettings.ShowLineNumbers;
			this.miConvertTabsToSpaces.Checked = ActiveEditor.ConvertTabsToSpaces;
		}

		private void miShowSpacesTabs_Click(object sender, EventArgs e)
		{
			TextEditorControl editor = ActiveEditor;
			if (editor == null) return;
			editor.ShowSpaces = editor.ShowTabs = !editor.ShowSpaces;
			OnSettingsChanged();
		}

		private void miShowEOLMarkers_Click(object sender, EventArgs e)
		{
			TextEditorControl editor = ActiveEditor;
			if (editor == null) return;
			editor.ShowEOLMarkers = !editor.ShowEOLMarkers;
			OnSettingsChanged();
		}

		private void miShowInvalidLines_Click(object sender, EventArgs e)
		{
			TextEditorControl editor = ActiveEditor;
			if (editor == null) return;
			editor.ShowInvalidLines = !editor.ShowInvalidLines;
			OnSettingsChanged();
		}

		private void miShowLineNumbers_Click(object sender, EventArgs e)
		{
			TextEditorControl editor = ActiveEditor;
			if (editor == null) return;
			editor.ShowLineNumbers = !editor.ShowLineNumbers;
			OnSettingsChanged();
		}

		private void miHLCurRow_Click(object sender, EventArgs e)
		{
			TextEditorControl editor = ActiveEditor;
			if (editor == null) return;
			editor.LineViewerStyle = editor.LineViewerStyle == LineViewerStyle.None
				? LineViewerStyle.FullRow : LineViewerStyle.None;
			OnSettingsChanged();
		}

		private void miBracketMatchingStyle_Click(object sender, EventArgs e)
		{
			TextEditorControl editor = ActiveEditor;
			if (editor == null) return;
			editor.BracketMatchingStyle = editor.BracketMatchingStyle == BracketMatchingStyle.After
				? BracketMatchingStyle.Before : BracketMatchingStyle.After;
			OnSettingsChanged();
		}

		private void miEnableVirtualSpace_Click(object sender, EventArgs e)
		{
			TextEditorControl editor = ActiveEditor;
			if (editor == null) return;
			editor.AllowCaretBeyondEOL = !editor.AllowCaretBeyondEOL;
			OnSettingsChanged();
		}

		private void miConvertTabsToSpaces_Click(object sender, EventArgs e)
		{
			TextEditorControl editor = ActiveEditor;
			if (editor == null) return;
			editor.ConvertTabsToSpaces = !editor.ConvertTabsToSpaces;
			OnSettingsChanged();
		}

		private void miSetTabSize_Click(object sender, EventArgs e)
		{
			if (ActiveEditor != null)
			{
				string result = InputBox.Show("请指定制表符大小：", "制表符大小", _editorSettings.TabIndent.ToString());
				int value;
				if (result != null && int.TryParse(result, out value) && Globals.IsInRange(value, 1, 32))
				{
					ActiveEditor.TabIndent = value;
				}
			}
		}

		private void miSetFont_Click(object sender, EventArgs e)
		{
			TextEditorControl editor = ActiveEditor;
			if (editor != null)
			{
				fontDialog.Font = editor.Font;
				if (fontDialog.ShowDialog(this) == DialogResult.OK)
				{
					editor.Font = fontDialog.Font;
					OnSettingsChanged();
				}
			}
		}

		/// <summary>Performs an action encapsulated in IEditAction.</summary>
		/// <remarks>
		/// There is an implementation of IEditAction for every action that 
		/// the user can invoke using a shortcut key (arrow keys, Ctrl+X, etc.)
		/// The editor control doesn't provide a public funciton to perform one
		/// of these actions directly, so I wrote DoEditAction() based on the
		/// code in TextArea.ExecuteDialogKey(). You can call ExecuteDialogKey
		/// directly, but it is more fragile because it takes a Keys value (e.g.
		/// Keys.Left) instead of the action to perform.
		/// <para/>
		/// Clipboard commands could also be done by calling methods in
		/// editor.ActiveTextAreaControl.TextArea.ClipboardHandler.
		/// </remarks>
		private void DoEditAction(TextEditorControl editor, ICSharpCode.TextEditor.Actions.IEditAction action)
		{
			if (editor != null && action != null)
			{
				TextArea area = editor.ActiveTextAreaControl.TextArea;
				editor.BeginUpdate();
				try
				{
					lock (editor.Document)
					{
						action.Execute(area);
						if (area.SelectionManager.HasSomethingSelected && area.AutoClearSelection /*&& caretchanged*/)
						{
							if (area.Document.TextEditorProperties.DocumentSelectionMode == DocumentSelectionMode.Normal)
							{
								area.SelectionManager.ClearSelection();
							}
						}
					}
				}
				finally
				{
					editor.EndUpdate();
					area.Caret.UpdateCaretPosition();
				}
			}
		}

		private bool HaveSelection()
		{
			TextEditorControl editor = ActiveEditor;
			return editor != null &&
				editor.ActiveTextAreaControl.TextArea.SelectionManager.HasSomethingSelected;
		}

		private void miEditCut_Click(object sender, EventArgs e)
		{
			if (HaveSelection())
				DoEditAction(ActiveEditor, new ICSharpCode.TextEditor.Actions.Cut());
		}

		private void miEditCopy_Click(object sender, EventArgs e)
		{
			if (HaveSelection())
				DoEditAction(ActiveEditor, new ICSharpCode.TextEditor.Actions.Copy());
		}

		private void miEditPaste_Click(object sender, EventArgs e)
		{
			DoEditAction(ActiveEditor, new ICSharpCode.TextEditor.Actions.Paste());
		}

		private void miEditDelete_Click(object sender, EventArgs e)
		{
			if (HaveSelection())
				DoEditAction(ActiveEditor, new ICSharpCode.TextEditor.Actions.Delete());
		}

		FindAndReplaceForm _findForm = new FindAndReplaceForm();

		private void miEditFind_Click(object sender, EventArgs e)
		{
			TextEditorControl editor = ActiveEditor;
			if (editor == null) return;
			_findForm.ShowFor(editor, false);
		}

		private void miEditReplace_Click(object sender, EventArgs e)
		{
			TextEditorControl editor = ActiveEditor;
			if (editor == null) return;
			_findForm.ShowFor(editor, true);
		}

		private void miEditFindNext_Click(object sender, EventArgs e)
		{
			_findForm.FindNext(true, false,
				string.Format("没有找到你要查找的内容！", _findForm.LookFor));
		}

		private void miEditFindPrev_Click(object sender, EventArgs e)
		{
			_findForm.FindNext(true, true,
				string.Format("没有找到你要查找的内容！", _findForm.LookFor));
		}

		private void miToggleBookmark_Click(object sender, EventArgs e)
		{
			TextEditorControl editor = ActiveEditor;
			if (editor != null)
			{
				DoEditAction(ActiveEditor, new ICSharpCode.TextEditor.Actions.ToggleBookmark());
				editor.IsIconBarVisible = editor.Document.BookmarkManager.Marks.Count > 0;
			}
		}

		private void miGoToNextBookmark_Click(object sender, EventArgs e)
		{
			DoEditAction(ActiveEditor, new ICSharpCode.TextEditor.Actions.GotoNextBookmark(new Predicate<Bookmark>(delegate(Bookmark bookmark)
			{
				return true;
			})));
		}

		private void miGoToPrevBookmark_Click(object sender, EventArgs e)
		{
			DoEditAction(ActiveEditor, new ICSharpCode.TextEditor.Actions.GotoPrevBookmark(new Predicate<Bookmark>(delegate(Bookmark bookmark)
			{
				return true;
			})));
		}

		private void miFormatXml_Click(object sender, EventArgs e)
		{
			try
			{
				FormatXml(ActiveEditor.Document.TextContent);
			}
			catch (System.Exception ex)
			{
				MessageBox.Show(String.Format("Xml格式化失败：{0}", ex.ToString()));
			}
		}
	}
}
