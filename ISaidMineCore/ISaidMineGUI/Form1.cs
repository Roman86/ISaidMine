using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISaidMineGUI
{
	public partial class fMain : Form
	{
		protected ISaidMineCore.Core minerKiller;
		public fMain()
		{
			InitializeComponent();
			this.minerKiller = new ISaidMineCore.Core(this, @"sgminer", @"C:\Program Files (x86)\sgminer-ltcrabbit-v3\logs\last.log", 15*60);
			this.minerKiller.KillingMiner += new ISaidMineCore.Core.KillingMinerEventHandler(minerKiller_KillingMiner);
			this.minerKiller.MinerProcessKilled += new ISaidMineCore.Core.MinerKilledEventHandler(minerKiller_MinerProcessKilled);
			this.minerKiller.MinerProcessAlive += new ISaidMineCore.Core.MinerAliveEventHandler(minerKiller_MinerProcessAlive);
		}

		void minerKiller_KillingMiner(object sender, ISaidMineCore.KillingMinerEventArgs e)
		{
			log("time to kill..."+e.ProcessName);
		}

		void minerKiller_MinerProcessAlive(object sender, ISaidMineCore.MinerAliveEventArgs e)
		{
			//log("alive");
		}

		void log(string text)
		{
			string[] itemDetails = { DateTime.Now.ToString(), text };
			ListViewItem item = new ListViewItem(itemDetails);
			this.listView1.Items.Add(item);
		}

		void minerKiller_MinerProcessKilled(object sender, ISaidMineCore.MinerKilledEventArgs e)
		{
			log("process killed: "+e.ProcessName);
		}

		private void btnExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void fMain_Shown(object sender, EventArgs e)
		{
			this.coreBindingSource.DataSource = this.minerKiller;
			this.minerKiller.StartWatching();
		}
	}
}
