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

namespace XmlPad
{
	partial class XmlEditor : System.Windows.Forms.Form
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XmlEditor));
			this.menuStrip = new System.Windows.Forms.MenuStrip();
			this.miFile = new System.Windows.Forms.ToolStripMenuItem();
			this.miOpenFile = new System.Windows.Forms.ToolStripMenuItem();
			this.miSave = new System.Windows.Forms.ToolStripMenuItem();
			this.miSaveAs = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.miExit = new System.Windows.Forms.ToolStripMenuItem();
			this.miEdit = new System.Windows.Forms.ToolStripMenuItem();
			this.miEditCut = new System.Windows.Forms.ToolStripMenuItem();
			this.miEditCopy = new System.Windows.Forms.ToolStripMenuItem();
			this.miEditPaste = new System.Windows.Forms.ToolStripMenuItem();
			this.miEditDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.miEditFind = new System.Windows.Forms.ToolStripMenuItem();
			this.miEditReplace = new System.Windows.Forms.ToolStripMenuItem();
			this.miEditFindNext = new System.Windows.Forms.ToolStripMenuItem();
			this.miEditFindPrev = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
			this.miToggleBookmark = new System.Windows.Forms.ToolStripMenuItem();
			this.miGoToNextBookmark = new System.Windows.Forms.ToolStripMenuItem();
			this.miGoToPrevBookmark = new System.Windows.Forms.ToolStripMenuItem();
			this.miOption = new System.Windows.Forms.ToolStripMenuItem();
			this.miSplitWindow = new System.Windows.Forms.ToolStripMenuItem();
			this.miShowSpacesTabs = new System.Windows.Forms.ToolStripMenuItem();
			this.miShowEOLMarkers = new System.Windows.Forms.ToolStripMenuItem();
			this.miShowInvalidLines = new System.Windows.Forms.ToolStripMenuItem();
			this.miShowLineNumbers = new System.Windows.Forms.ToolStripMenuItem();
			this.miHLCurRow = new System.Windows.Forms.ToolStripMenuItem();
			this.miBracketMatchingStyle = new System.Windows.Forms.ToolStripMenuItem();
			this.miEnableVirtualSpace = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
			this.miConvertTabsToSpaces = new System.Windows.Forms.ToolStripMenuItem();
			this.miSetTabSize = new System.Windows.Forms.ToolStripMenuItem();
			this.miSetFont = new System.Windows.Forms.ToolStripMenuItem();
			this.miMenuXml = new System.Windows.Forms.ToolStripMenuItem();
			this.miViewMode = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
			this.miFormatXml = new System.Windows.Forms.ToolStripMenuItem();
			this.查看修改说明ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
			this.fontDialog = new System.Windows.Forms.FontDialog();
			this.textEditorControl = new ICSharpCode.TextEditor.TextEditorControl();
			this.menuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip
			// 
			this.menuStrip.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.miFile,
									this.miEdit,
									this.miOption,
									this.miMenuXml,
									this.查看修改说明ToolStripMenuItem});
			this.menuStrip.Location = new System.Drawing.Point(0, 0);
			this.menuStrip.Name = "menuStrip";
			this.menuStrip.Size = new System.Drawing.Size(624, 24);
			this.menuStrip.TabIndex = 0;
			this.menuStrip.Text = "menuStrip";
			// 
			// miFile
			// 
			this.miFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.miOpenFile,
									this.miSave,
									this.miSaveAs,
									this.toolStripMenuItem1,
									this.miExit});
			this.miFile.Name = "miFile";
			this.miFile.Size = new System.Drawing.Size(59, 20);
			this.miFile.Text = "文件(&F)";
			// 
			// miOpenFile
			// 
			this.miOpenFile.Image = ((System.Drawing.Image)(resources.GetObject("miOpenFile.Image")));
			this.miOpenFile.Name = "miOpenFile";
			this.miOpenFile.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.miOpenFile.Size = new System.Drawing.Size(171, 22);
			this.miOpenFile.Text = "打开(&O)...";
			this.miOpenFile.Click += new System.EventHandler(this.miOpen_Click);
			// 
			// miSave
			// 
			this.miSave.Image = ((System.Drawing.Image)(resources.GetObject("miSave.Image")));
			this.miSave.Name = "miSave";
			this.miSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.miSave.Size = new System.Drawing.Size(171, 22);
			this.miSave.Text = "保存(&S)";
			this.miSave.Click += new System.EventHandler(this.miSave_Click);
			// 
			// miSaveAs
			// 
			this.miSaveAs.Name = "miSaveAs";
			this.miSaveAs.Size = new System.Drawing.Size(171, 22);
			this.miSaveAs.Text = "另存为(&A)...";
			this.miSaveAs.Click += new System.EventHandler(this.miSaveAs_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(168, 6);
			// 
			// miExit
			// 
			this.miExit.Name = "miExit";
			this.miExit.Size = new System.Drawing.Size(171, 22);
			this.miExit.Text = "退出(&X)";
			this.miExit.Click += new System.EventHandler(this.miExit_Click);
			// 
			// miEdit
			// 
			this.miEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.miEditCut,
									this.miEditCopy,
									this.miEditPaste,
									this.miEditDelete,
									this.toolStripMenuItem2,
									this.miEditFind,
									this.miEditReplace,
									this.miEditFindNext,
									this.miEditFindPrev,
									this.toolStripMenuItem3,
									this.miToggleBookmark,
									this.miGoToNextBookmark,
									this.miGoToPrevBookmark});
			this.miEdit.Name = "miEdit";
			this.miEdit.Size = new System.Drawing.Size(59, 20);
			this.miEdit.Text = "编辑(&E)";
			// 
			// miEditCut
			// 
			this.miEditCut.Image = ((System.Drawing.Image)(resources.GetObject("miEditCut.Image")));
			this.miEditCut.Name = "miEditCut";
			this.miEditCut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
			this.miEditCut.Size = new System.Drawing.Size(201, 22);
			this.miEditCut.Text = "剪切(&X)";
			this.miEditCut.Click += new System.EventHandler(this.miEditCut_Click);
			// 
			// miEditCopy
			// 
			this.miEditCopy.Image = ((System.Drawing.Image)(resources.GetObject("miEditCopy.Image")));
			this.miEditCopy.Name = "miEditCopy";
			this.miEditCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
			this.miEditCopy.Size = new System.Drawing.Size(201, 22);
			this.miEditCopy.Text = "复制(&C)";
			this.miEditCopy.Click += new System.EventHandler(this.miEditCopy_Click);
			// 
			// miEditPaste
			// 
			this.miEditPaste.Image = ((System.Drawing.Image)(resources.GetObject("miEditPaste.Image")));
			this.miEditPaste.Name = "miEditPaste";
			this.miEditPaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
			this.miEditPaste.Size = new System.Drawing.Size(201, 22);
			this.miEditPaste.Text = "粘贴(&V)";
			this.miEditPaste.Click += new System.EventHandler(this.miEditPaste_Click);
			// 
			// miEditDelete
			// 
			this.miEditDelete.Image = ((System.Drawing.Image)(resources.GetObject("miEditDelete.Image")));
			this.miEditDelete.Name = "miEditDelete";
			this.miEditDelete.Size = new System.Drawing.Size(201, 22);
			this.miEditDelete.Text = "删除(&D)";
			this.miEditDelete.Click += new System.EventHandler(this.miEditDelete_Click);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(198, 6);
			// 
			// miEditFind
			// 
			this.miEditFind.Image = ((System.Drawing.Image)(resources.GetObject("miEditFind.Image")));
			this.miEditFind.Name = "miEditFind";
			this.miEditFind.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
			this.miEditFind.Size = new System.Drawing.Size(201, 22);
			this.miEditFind.Text = "查找(&F)...";
			this.miEditFind.Click += new System.EventHandler(this.miEditFind_Click);
			// 
			// miEditReplace
			// 
			this.miEditReplace.Name = "miEditReplace";
			this.miEditReplace.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
			this.miEditReplace.Size = new System.Drawing.Size(201, 22);
			this.miEditReplace.Text = "替换(&R)...";
			this.miEditReplace.Click += new System.EventHandler(this.miEditReplace_Click);
			// 
			// miEditFindNext
			// 
			this.miEditFindNext.Image = ((System.Drawing.Image)(resources.GetObject("miEditFindNext.Image")));
			this.miEditFindNext.Name = "miEditFindNext";
			this.miEditFindNext.ShortcutKeys = System.Windows.Forms.Keys.F3;
			this.miEditFindNext.Size = new System.Drawing.Size(201, 22);
			this.miEditFindNext.Text = "查找下一个(&N)";
			this.miEditFindNext.Click += new System.EventHandler(this.miEditFindNext_Click);
			// 
			// miEditFindPrev
			// 
			this.miEditFindPrev.Name = "miEditFindPrev";
			this.miEditFindPrev.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F3)));
			this.miEditFindPrev.Size = new System.Drawing.Size(201, 22);
			this.miEditFindPrev.Text = "查找上一个(&P)";
			this.miEditFindPrev.Click += new System.EventHandler(this.miEditFindPrev_Click);
			// 
			// toolStripMenuItem3
			// 
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new System.Drawing.Size(198, 6);
			// 
			// miToggleBookmark
			// 
			this.miToggleBookmark.Name = "miToggleBookmark";
			this.miToggleBookmark.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F2)));
			this.miToggleBookmark.Size = new System.Drawing.Size(201, 22);
			this.miToggleBookmark.Text = "设置/取消书签";
			this.miToggleBookmark.Click += new System.EventHandler(this.miToggleBookmark_Click);
			// 
			// miGoToNextBookmark
			// 
			this.miGoToNextBookmark.Name = "miGoToNextBookmark";
			this.miGoToNextBookmark.ShortcutKeys = System.Windows.Forms.Keys.F2;
			this.miGoToNextBookmark.Size = new System.Drawing.Size(201, 22);
			this.miGoToNextBookmark.Text = "转到下一书签";
			this.miGoToNextBookmark.Click += new System.EventHandler(this.miGoToNextBookmark_Click);
			// 
			// miGoToPrevBookmark
			// 
			this.miGoToPrevBookmark.Name = "miGoToPrevBookmark";
			this.miGoToPrevBookmark.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F2)));
			this.miGoToPrevBookmark.Size = new System.Drawing.Size(201, 22);
			this.miGoToPrevBookmark.Text = "转到前一书签";
			this.miGoToPrevBookmark.Click += new System.EventHandler(this.miGoToPrevBookmark_Click);
			// 
			// miOption
			// 
			this.miOption.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.miSplitWindow,
									this.miShowSpacesTabs,
									this.miShowEOLMarkers,
									this.miShowInvalidLines,
									this.miShowLineNumbers,
									this.miHLCurRow,
									this.miBracketMatchingStyle,
									this.miEnableVirtualSpace,
									this.toolStripMenuItem4,
									this.miConvertTabsToSpaces,
									this.miSetTabSize,
									this.miSetFont});
			this.miOption.Name = "miOption";
			this.miOption.Size = new System.Drawing.Size(59, 20);
			this.miOption.Text = "选项(&O)";
			// 
			// miSplitWindow
			// 
			this.miSplitWindow.Image = ((System.Drawing.Image)(resources.GetObject("miSplitWindow.Image")));
			this.miSplitWindow.Name = "miSplitWindow";
			this.miSplitWindow.Size = new System.Drawing.Size(244, 22);
			this.miSplitWindow.Text = "拆分窗口(&W)";
			this.miSplitWindow.Click += new System.EventHandler(this.miSplitWindow_Click);
			// 
			// miShowSpacesTabs
			// 
			this.miShowSpacesTabs.Name = "miShowSpacesTabs";
			this.miShowSpacesTabs.Size = new System.Drawing.Size(244, 22);
			this.miShowSpacesTabs.Text = "显示空格和制表符(&S)";
			this.miShowSpacesTabs.Click += new System.EventHandler(this.miShowSpacesTabs_Click);
			// 
			// miShowEOLMarkers
			// 
			this.miShowEOLMarkers.Name = "miShowEOLMarkers";
			this.miShowEOLMarkers.Size = new System.Drawing.Size(244, 22);
			this.miShowEOLMarkers.Text = "显示换行标记(&E)";
			this.miShowEOLMarkers.Click += new System.EventHandler(this.miShowEOLMarkers_Click);
			// 
			// miShowInvalidLines
			// 
			this.miShowInvalidLines.Name = "miShowInvalidLines";
			this.miShowInvalidLines.Size = new System.Drawing.Size(244, 22);
			this.miShowInvalidLines.Text = "显示无效行标记(&I)";
			this.miShowInvalidLines.Click += new System.EventHandler(this.miShowInvalidLines_Click);
			// 
			// miShowLineNumbers
			// 
			this.miShowLineNumbers.Name = "miShowLineNumbers";
			this.miShowLineNumbers.Size = new System.Drawing.Size(244, 22);
			this.miShowLineNumbers.Text = "显示行号(&L)";
			this.miShowLineNumbers.Click += new System.EventHandler(this.miShowLineNumbers_Click);
			// 
			// miHLCurRow
			// 
			this.miHLCurRow.Image = ((System.Drawing.Image)(resources.GetObject("miHLCurRow.Image")));
			this.miHLCurRow.Name = "miHLCurRow";
			this.miHLCurRow.Size = new System.Drawing.Size(244, 22);
			this.miHLCurRow.Text = "高亮当前行(&H)";
			this.miHLCurRow.Click += new System.EventHandler(this.miHLCurRow_Click);
			// 
			// miBracketMatchingStyle
			// 
			this.miBracketMatchingStyle.Name = "miBracketMatchingStyle";
			this.miBracketMatchingStyle.Size = new System.Drawing.Size(244, 22);
			this.miBracketMatchingStyle.Text = "高亮匹配括号当光标在其后时(&A)";
			this.miBracketMatchingStyle.Visible = false;
			this.miBracketMatchingStyle.Click += new System.EventHandler(this.miBracketMatchingStyle_Click);
			// 
			// miEnableVirtualSpace
			// 
			this.miEnableVirtualSpace.Name = "miEnableVirtualSpace";
			this.miEnableVirtualSpace.Size = new System.Drawing.Size(244, 22);
			this.miEnableVirtualSpace.Text = "启用虚空格(&V)";
			this.miEnableVirtualSpace.Click += new System.EventHandler(this.miEnableVirtualSpace_Click);
			// 
			// toolStripMenuItem4
			// 
			this.toolStripMenuItem4.Name = "toolStripMenuItem4";
			this.toolStripMenuItem4.Size = new System.Drawing.Size(241, 6);
			// 
			// miConvertTabsToSpaces
			// 
			this.miConvertTabsToSpaces.Name = "miConvertTabsToSpaces";
			this.miConvertTabsToSpaces.Size = new System.Drawing.Size(244, 22);
			this.miConvertTabsToSpaces.Text = "制表符转换为空格(&C)";
			this.miConvertTabsToSpaces.Click += new System.EventHandler(this.miConvertTabsToSpaces_Click);
			// 
			// miSetTabSize
			// 
			this.miSetTabSize.Name = "miSetTabSize";
			this.miSetTabSize.Size = new System.Drawing.Size(244, 22);
			this.miSetTabSize.Text = "设置制表符大小(&T)";
			this.miSetTabSize.Click += new System.EventHandler(this.miSetTabSize_Click);
			// 
			// miSetFont
			// 
			this.miSetFont.Image = ((System.Drawing.Image)(resources.GetObject("miSetFont.Image")));
			this.miSetFont.Name = "miSetFont";
			this.miSetFont.Size = new System.Drawing.Size(244, 22);
			this.miSetFont.Text = "字体(&F)";
			this.miSetFont.Click += new System.EventHandler(this.miSetFont_Click);
			// 
			// miMenuXml
			// 
			this.miMenuXml.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.miViewMode,
									this.toolStripMenuItem6,
									this.miFormatXml});
			this.miMenuXml.Name = "miMenuXml";
			this.miMenuXml.Size = new System.Drawing.Size(53, 20);
			this.miMenuXml.Text = "XML(&X)";
			// 
			// miViewMode
			// 
			this.miViewMode.Name = "miViewMode";
			this.miViewMode.Size = new System.Drawing.Size(142, 22);
			this.miViewMode.Text = "查看方式(&M)";
			// 
			// toolStripMenuItem6
			// 
			this.toolStripMenuItem6.Name = "toolStripMenuItem6";
			this.toolStripMenuItem6.Size = new System.Drawing.Size(139, 6);
			// 
			// miFormatXml
			// 
			this.miFormatXml.Name = "miFormatXml";
			this.miFormatXml.Size = new System.Drawing.Size(142, 22);
			this.miFormatXml.Text = "Xml格式化(&F)";
			this.miFormatXml.Click += new System.EventHandler(this.miFormatXml_Click);
			// 
			// 查看修改说明ToolStripMenuItem
			// 
			this.查看修改说明ToolStripMenuItem.Name = "查看修改说明ToolStripMenuItem";
			this.查看修改说明ToolStripMenuItem.Size = new System.Drawing.Size(89, 20);
			this.查看修改说明ToolStripMenuItem.Text = "查看修改说明";
			this.查看修改说明ToolStripMenuItem.Click += new System.EventHandler(this.查看修改说明ToolStripMenuItemClick);
			// 
			// toolStripMenuItem5
			// 
			this.toolStripMenuItem5.Name = "toolStripMenuItem5";
			this.toolStripMenuItem5.Size = new System.Drawing.Size(241, 6);
			// 
			// textEditorControl
			// 
			this.textEditorControl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.textEditorControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textEditorControl.IsReadOnly = false;
			this.textEditorControl.Location = new System.Drawing.Point(0, 24);
			this.textEditorControl.Name = "textEditorControl";
			this.textEditorControl.Size = new System.Drawing.Size(624, 420);
			this.textEditorControl.TabIndex = 0;
			// 
			// XmlEditor
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.ClientSize = new System.Drawing.Size(624, 444);
			this.Controls.Add(this.textEditorControl);
			this.Controls.Add(this.menuStrip);
			this.DoubleBuffered = true;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip;
			this.Name = "XmlEditor";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Xml编辑器";
			this.menuStrip.ResumeLayout(false);
			this.menuStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.ToolStripMenuItem 查看修改说明ToolStripMenuItem;
		private System.Windows.Forms.MenuStrip menuStrip;
		private System.Windows.Forms.ToolStripMenuItem miFile;
		private System.Windows.Forms.ToolStripMenuItem miOpenFile;
		private System.Windows.Forms.ToolStripMenuItem miSave;
		private System.Windows.Forms.ToolStripMenuItem miSaveAs;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem miExit;
		private System.Windows.Forms.ToolStripMenuItem miEdit;
		private System.Windows.Forms.ToolStripMenuItem miEditCut;
		private System.Windows.Forms.ToolStripMenuItem miEditCopy;
		private System.Windows.Forms.ToolStripMenuItem miEditPaste;
		private System.Windows.Forms.ToolStripMenuItem miEditDelete;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem miEditFind;
		private System.Windows.Forms.ToolStripMenuItem miEditReplace;
		private System.Windows.Forms.ToolStripMenuItem miEditFindNext;
		private System.Windows.Forms.ToolStripMenuItem miEditFindPrev;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
		private System.Windows.Forms.ToolStripMenuItem miToggleBookmark;
		private System.Windows.Forms.ToolStripMenuItem miGoToNextBookmark;
		private System.Windows.Forms.ToolStripMenuItem miGoToPrevBookmark;
		private System.Windows.Forms.ToolStripMenuItem miOption;
		private System.Windows.Forms.ToolStripMenuItem miViewMode;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
		private System.Windows.Forms.ToolStripMenuItem miSplitWindow;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
		private System.Windows.Forms.ToolStripMenuItem miShowSpacesTabs;
		private System.Windows.Forms.ToolStripMenuItem miShowEOLMarkers;
		private System.Windows.Forms.ToolStripMenuItem miShowInvalidLines;
		private System.Windows.Forms.ToolStripMenuItem miShowLineNumbers;
		private System.Windows.Forms.ToolStripMenuItem miHLCurRow;
		private System.Windows.Forms.ToolStripMenuItem miBracketMatchingStyle;
		private System.Windows.Forms.ToolStripMenuItem miEnableVirtualSpace;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
		private System.Windows.Forms.ToolStripMenuItem miSetTabSize;
		private System.Windows.Forms.ToolStripMenuItem miSetFont;
		private System.Windows.Forms.FontDialog fontDialog;
		private ICSharpCode.TextEditor.TextEditorControl textEditorControl;
		private System.Windows.Forms.ToolStripMenuItem miMenuXml;
		private System.Windows.Forms.ToolStripMenuItem miFormatXml;
		private System.Windows.Forms.ToolStripMenuItem miConvertTabsToSpaces;
		
		
		
		
	}
}
