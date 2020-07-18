// Decompiled with JetBrains decompiler
// Type: ADTechnology.Windows.Controls.OpenHostFileDialogLocations
// Assembly: OpenHostFileDialog1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=e9ef268d26c5f334
// MVID: F8D282FE-1A5F-453A-946F-DF9521E5E170
// Assembly location: C:\Users\DaveAndTina\Documents\Projects\ailog installation\Application Files\ailog_1_0_0_7\OpenHostFileDialog1.dll

using ADTechnology.Classes;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace ADTechnology.Apps.OpenHostFileDialog
{
  public partial class OpenHostFileDialogLocations : Form
  {

    public string LocationName
    {
      get
      {
        return this.tbName.Text;
      }
      set
      {
        this.tbName.Text = value;
      }
    }

    public string Host
    {
      get
      {
        return this.lblHost.Text;
      }
      set
      {
        this.lblHost.Text = value;
      }
    }

    public string User
    {
      get
      {
        return this.lblUser.Text;
      }
      set
      {
        this.lblUser.Text = value;
      }
    }

    public string Pass
    {
      get
      {
        return this.pass;
      }
      set
      {
        this.pass = value;
      }
    }

    public string Path
    {
      get
      {
        return this.lblPath.Text;
      }
      set
      {
        this.lblPath.Text = value;
      }
    }

    public OpenHostFileDialogLocations()
    {
      this.InitializeComponent();
      this.fileName = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + Application.CompanyName + "\\" + Application.ProductName + "_Locations.xml";
      this.buildList();
    }

    private void buildList()
    {
      FileStream fileStream = (FileStream) null;
      try
      {
        fileStream = new FileStream(this.fileName, FileMode.Open, FileAccess.Read);
        this.hostFile = HostLocationFile.ReadXml((Stream) fileStream);
      }
      catch (DirectoryNotFoundException ex)
      {
        Directory.CreateDirectory(System.IO.Path.GetDirectoryName(this.fileName));
        this.hostFile = new HostLocationFile(Application.ProductName);
      }
      catch (FileNotFoundException ex)
      {
        this.hostFile = new HostLocationFile(Application.ProductName);
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show(ex.Message + "\n\n" + ex.GetType().ToString(), "Error reading locations");
        this.hostFile = new HostLocationFile(Application.ProductName);
      }
      finally
      {
        fileStream?.Close();
      }
      this.bind_ds();
    }

    private void bind_ds()
    {
      this.bind = new BindingSource();
      this.bind.DataSource = (object) this.hostFile.HostLocations;
      this.dgMain.AutoGenerateColumns = false;
      this.dgMain.DataSource = (object) this.bind;
      this.dgMain.ClearSelection();
    }

    private void find_loc()
    {
      int index = 0;
      foreach (HostLocation hostLocation in (CollectionBase) this.hostFile.HostLocations)
      {
        if (hostLocation.Host == this.lblHost.Text && hostLocation.User == this.lblUser.Text && hostLocation.Path == this.lblPath.Text)
        {
          this.dgMain.CurrentCell = this.dgMain.Rows[index].Cells[1];
          break;
        }
        ++index;
      }
    }

    private void add_to_ds()
    {
      HostLocation HostLocation = new HostLocation(this.tbName.Text, this.lblHost.Text, this.lblUser.Text, this.pass, this.lblPath.Text);
      HostLocation.Encrypt(HostLocation.User, Application.ProductName);
      this.hostFile.HostLocations.AddOrUpdate(HostLocation);
      this.bind_ds();
      this.find_loc();
    }

    private void remove_from_ds()
    {
      this.hostFile.HostLocations.Remove((HostLocation) this.dgMain.CurrentRow.DataBoundItem);
      this.bind_ds();
    }

    private void persist_ds()
    {
      FileStream fileStream = (FileStream) null;
      try
      {
        fileStream = new FileStream(this.fileName, FileMode.Create, FileAccess.Write);
        byte[] bytes = Encoding.UTF8.GetBytes(this.hostFile.WriteXml());
        fileStream.Write(bytes, 0, bytes.Length);
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show(ex.Message + "\n\n" + ex.GetType().ToString(), "Error saving locations");
        this.DialogResult = DialogResult.Abort;
      }
      finally
      {
        fileStream?.Close();
      }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
    }

    private void OpenHostFileDialogLocations_Activated(object sender, EventArgs e)
    {
      this.dgMain.ClearSelection();
      this.dgMain.SelectionChanged += new EventHandler(this.dgMain_SelectionChanged);
      this.btnOpen.Enabled = false;
      if (this.lblHost.Text.Trim().Length > 0 && this.lblUser.Text.Trim().Length > 0 && this.lblPath.Text.Trim().Length > 0)
        this.find_loc();
      if (this.lblHost.Text.Trim() == "" || this.lblUser.Text.Trim() == "" || this.pass.Trim() == "")
        this.btnSave.Enabled = false;
      else
        this.btnSave.Enabled = true;
    }

    private void OpenHostFileDialogLocations_Deactivate(object sender, EventArgs e)
    {
      this.dgMain.SelectionChanged -= new EventHandler(this.dgMain_SelectionChanged);
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      this.add_to_ds();
      this.persist_ds();
      this.DialogResult = DialogResult.Ignore;
    }

    private void dgMain_SelectionChanged(object sender, EventArgs e)
    {
      if (this.dgMain.SelectedRows.Count > 0)
      {
        HostLocation dataBoundItem = (HostLocation) this.dgMain.SelectedRows[0].DataBoundItem;
        this.tbName.Text = dataBoundItem.Name;
        this.lblHost.Text = dataBoundItem.Host;
        this.lblUser.Text = dataBoundItem.User;
        this.lblPath.Text = dataBoundItem.Path;
        this.btnOpen.Enabled = this.btnSave.Enabled = true;
      }
      else
        this.btnOpen.Enabled = false;
    }

    private void btnOpen_Click(object sender, EventArgs e)
    {
      if (this.dgMain.SelectedRows.Count <= 0)
        return;
      HostLocation dataBoundItem = (HostLocation) this.dgMain.SelectedRows[0].DataBoundItem;
      this.tbName.Text = dataBoundItem.Name;
      this.lblHost.Text = dataBoundItem.Host;
      this.lblUser.Text = dataBoundItem.User;
      this.lblPath.Text = dataBoundItem.Path;
      try
      {
        this.pass = dataBoundItem.Decrypt(this.lblUser.Text, Application.ProductName);
      }
      catch (EncryptedStringException ex)
      {
        MessageBox.Show(ex.Message, "Password Decryption Error");
        this.DialogResult = DialogResult.Abort;
      }
      this.DialogResult = DialogResult.OK;
    }

    private void dgMain_DoubleClick(object sender, EventArgs e)
    {
      this.btnOpen_Click(sender, e);
    }

    private void dgMain_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
    {
      if (e.Button != MouseButtons.Right || e.RowIndex <= -1)
        return;
      this.dgMain.CurrentCell = this.dgMain[e.ColumnIndex, e.RowIndex];
    }

    private void miDelete_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show("Are you sure you wish to permanently delete the selected location?", "Delete Location", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
        return;
      this.remove_from_ds();
      this.persist_ds();
    }

    private void dgMain_MouseDown(object sender, MouseEventArgs e)
    {
      this.dgMain.ContextMenuStrip = (ContextMenuStrip) null;
      if (e.Button != MouseButtons.Right || this.dgMain.HitTest(e.X, e.Y).RowIndex <= -1)
        return;
      this.dgMain.ContextMenuStrip = this.cmsMain;
    }
  }
}
