#pragma once


namespace T4Examplecpp {

	using namespace System;
	using namespace System::ComponentModel;
	using namespace System::Collections;
	using namespace System::Windows::Forms;
	using namespace System::Data;
	using namespace System::Drawing;
	using namespace T4;
	using namespace T4::API;


	/// <summary>
	/// Summary for frmMain
	///
	/// WARNING: If you change the name of this class, you will need to change the
	///          'Resource File Name' property for the managed resource compiler tool
	///          associated with all .resx files this class depends on.  Otherwise,
	///          the designers will not be able to interact properly with localized
	///          resources associated with this form.
	/// </summary>
	public ref class frmMain : public System::Windows::Forms::Form
	{		
		//Reference to the main api host object.
		Host ^moHost;

		//Reference to the market list filter that we request.
		MarketList ^moFilter;

		//Reference to the accounts list.
		AccountList ^moAccounts;

		//Reference to a submitted order.
		Order ^moOrder;

		//Reference to the batch orders.
		Order ^moOrder1;
		Order ^moOrder2;

	private: 
		System::Windows::Forms::Button^  cmdBatchRevision;
		System::Windows::Forms::Button^  cmdBatchSubmit;
		System::Windows::Forms::Button^  cmdSinglePull;
		System::Windows::Forms::Button^  cmdSingleRevision;
		System::Windows::Forms::Button^  cmdSingleSubmit;	
		System::Windows::Forms::Button^  cmdBatchPull;


	public:
		frmMain(void)
		{
			InitializeComponent();
			
		}

	protected:
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		~frmMain()
		{
			
			if (components)
			{
				delete components;
			}
		}
	
	private:
		/// <summary>
		/// Required designer variable.
		/// </summary>
		System::ComponentModel::Container ^components;

#pragma region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>

		

		void InitializeComponent(void)
		{
			this->cmdBatchPull = (gcnew System::Windows::Forms::Button());
			this->cmdBatchRevision = (gcnew System::Windows::Forms::Button());
			this->cmdBatchSubmit = (gcnew System::Windows::Forms::Button());
			this->cmdSinglePull = (gcnew System::Windows::Forms::Button());
			this->cmdSingleRevision = (gcnew System::Windows::Forms::Button());
			this->cmdSingleSubmit = (gcnew System::Windows::Forms::Button());
			this->SuspendLayout();
			// 
			// cmdBatchPull
			// 
			this->cmdBatchPull->Location = System::Drawing::Point(170, 165);
			this->cmdBatchPull->Name = L"cmdBatchPull";
			this->cmdBatchPull->Size = System::Drawing::Size(88, 23);
			this->cmdBatchPull->TabIndex = 11;
			this->cmdBatchPull->Text = L"Batch Pull";
			this->cmdBatchPull->Click += gcnew System::EventHandler(this, &frmMain::cmdBatchPull_Click);
			// 
			// cmdBatchRevision
			// 
			this->cmdBatchRevision->Location = System::Drawing::Point(170, 125);
			this->cmdBatchRevision->Name = L"cmdBatchRevision";
			this->cmdBatchRevision->Size = System::Drawing::Size(88, 23);
			this->cmdBatchRevision->TabIndex = 10;
			this->cmdBatchRevision->Text = L"Batch Revision";
			this->cmdBatchRevision->Click += gcnew System::EventHandler(this, &frmMain::cmdBatchRevision_Click);
			// 
			// cmdBatchSubmit
			// 
			this->cmdBatchSubmit->Location = System::Drawing::Point(170, 85);
			this->cmdBatchSubmit->Name = L"cmdBatchSubmit";
			this->cmdBatchSubmit->Size = System::Drawing::Size(88, 23);
			this->cmdBatchSubmit->TabIndex = 9;
			this->cmdBatchSubmit->Text = L"Batch Submit";
			this->cmdBatchSubmit->Click += gcnew System::EventHandler(this, &frmMain::cmdBatchSubmit_Click);
			// 
			// cmdSinglePull
			// 
			this->cmdSinglePull->Location = System::Drawing::Point(34, 165);
			this->cmdSinglePull->Name = L"cmdSinglePull";
			this->cmdSinglePull->Size = System::Drawing::Size(96, 23);
			this->cmdSinglePull->TabIndex = 8;
			this->cmdSinglePull->Text = L"Single Pull";
			this->cmdSinglePull->Click += gcnew System::EventHandler(this, &frmMain::cmdSinglePull_Click);
			// 
			// cmdSingleRevision
			// 
			this->cmdSingleRevision->Location = System::Drawing::Point(34, 125);
			this->cmdSingleRevision->Name = L"cmdSingleRevision";
			this->cmdSingleRevision->Size = System::Drawing::Size(96, 23);
			this->cmdSingleRevision->TabIndex = 7;
			this->cmdSingleRevision->Text = L"Single Revision";
			this->cmdSingleRevision->Click += gcnew System::EventHandler(this, &frmMain::cmdSingleRevision_Click);
			// 
			// cmdSingleSubmit
			// 
			this->cmdSingleSubmit->Location = System::Drawing::Point(34, 85);
			this->cmdSingleSubmit->Name = L"cmdSingleSubmit";
			this->cmdSingleSubmit->Size = System::Drawing::Size(96, 23);
			this->cmdSingleSubmit->TabIndex = 6;
			this->cmdSingleSubmit->Text = L"Single Submit";
			this->cmdSingleSubmit->Click += gcnew System::EventHandler(this, &frmMain::cmdSingleSubmit_Click);
			// 
			// frmMain
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(6, 13);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->ClientSize = System::Drawing::Size(292, 273);
			this->Controls->Add(this->cmdBatchPull);
			this->Controls->Add(this->cmdBatchRevision);
			this->Controls->Add(this->cmdBatchSubmit);
			this->Controls->Add(this->cmdSinglePull);
			this->Controls->Add(this->cmdSingleRevision);
			this->Controls->Add(this->cmdSingleSubmit);
			this->Name = L"frmMain";
			this->Text = L"frmMain";
			this->Load += gcnew System::EventHandler(this, &frmMain::frmMain_Load);
			this->FormClosed += gcnew System::Windows::Forms::FormClosedEventHandler(this, &frmMain::frmMain_FormClosed);
			this->ResumeLayout(false);

		}
#pragma endregion
			
#pragma region Login Result

		void LoginSuccessEventHandler()
		{
			Diagnostics::Debug::WriteLine("Login success.");
			//Subscribe to a market for depth.
			SubscribeToMarket();

			//Subscribe to an account.
			SubscribeToAccount();

		}

		void LoginFailureEventHandler(T4::LoginResult penReason)
		{
			Diagnostics::Debug::WriteLine("Login failed due to " + penReason.ToString());
		}

#pragma endregion

#pragma region Getting Market Data

		//Method called following login success to get the data for 
		//a market and subscribe to it.
		void SubscribeToMarket()
		{
			//Request all the 'N' contracts on the Dummy exchange 
			//on the simulator.
			moFilter = moHost->MarketData->CreateMarketFilter("D_D", "N");

			//hook event --- C++ code
			moFilter->MarketListComplete +=
				gcnew MarketList::MarketListCompleteEventHandler(this, 
				&frmMain::MarketListCompleteEventHandler);
        
			//Check to see if the data is already loaded.
			if  (moFilter->Complete )
				//Call the event handler ourselves as the data is 
				//already loaded.
				MarketListCompleteEventHandler(moFilter);

		}
		
		//Event handler for event raised when the collection data has been loaded.
		void MarketListCompleteEventHandler(MarketList ^poMarketList)
		{
			//Display the name of each market loaded.
			for each(Market ^poMarket in poMarketList)
			{
				Diagnostics::Debug::WriteLine(poMarket->Description);

				//Subscribe to the market.
				poMarket->DepthSubscribe(DepthBuffer::Smart,DepthLevels::Normal);

				//hook events --- C++ code
				poMarket->MarketDepthUpdate += 
					gcnew Market::MarketDepthUpdateEventHandler(this, 
					&frmMain::MarketDepthUpdateEventHandler);
				poMarket->MarketHighLow +=
					gcnew Market::MarketHighLowEventHandler(this,
					&frmMain::MarketHighLowEventHandler);
				poMarket->MarketSettlement +=
					gcnew Market::MarketSettlementEventHandler( this,
					&frmMain::MarketSettlementEventHandler);
				poMarket->MarketTradeVolume +=
					gcnew Market::MarketTradeVolumeEventHandler(this, 
					&frmMain::MarketTradeVolumeEventHandler);

			}
		}
		
		//Event handler for event raised when there is a new depth update for the market.
		void MarketDepthUpdateEventHandler(Market ^poMarket)
		{
			//Display the Depth Update
			String^ sText;
			sText = "Depth Update: " + poMarket->Description;

			//Display some of the depth details.
			sText = sText + ", LastTrade: "+
            poMarket->LastDepth->LastTradeVolume + "@" +
            poMarket->ConvertTicksDisplay(poMarket->LastDepth->LastTradeTicks);

			//Display the data
			Diagnostics::Debug::WriteLine(sText);

		}
		
		//Event handler for event raised when there is a new high low for the market.
		void MarketHighLowEventHandler(Market ^poMarket)
		{
			//Display the high and low price.
			Diagnostics::Debug::WriteLine(poMarket->Description + ", " +
				poMarket->ConvertTicksDisplay(poMarket->LastHighLow->HighTicks) + " - " +
				poMarket->ConvertTicksDisplay(poMarket->LastHighLow->LowTicks));
		}
		
		//Event handler for event raised when there is a new settlement price for the Market.
        void MarketSettlementEventHandler(Market ^poMarket)
		{
			//Display the settlement price.
			Diagnostics::Debug::WriteLine("Settlement: " + poMarket->Description + ", " +
				poMarket->ConvertTicksDisplay(poMarket->LastSettlement->Ticks));
		}

		//Event handler for event raised when there are new total trade volume data for the market.
		void MarketTradeVolumeEventHandler(Market ^poMarket, Market::TradeVolume ^poChanges)
		{
			//Display the trade volume changes.
			String^ sText;
			sText = "TradeVolume: " + poMarket->Description;

			Market::TradeVolume::VolumeItem ^poVolume;

			//Display some of the depth details.
			for (int nItem = 0; nItem < poChanges->Count; ++nItem)
			{
				poVolume = poChanges[nItem];
				sText = sText + ", "+ poVolume->Volume + "@" +
					poMarket->ConvertTicksDisplay(poVolume->Ticks);
			}
			
			//Display the data
			Diagnostics::Debug::WriteLine(sText);
		}       
   
#pragma endregion

#pragma region Getting Account Data

		//Method called following login success to get the data for 
		//an account and subscribe to it.
		void SubscribeToAccount()
		{
			//Set the account list reference so that we can get 
			//Account and order events.
			moAccounts = moHost->Accounts;

			//hook events --- C++ code
			moAccounts->AccountDetails += gcnew AccountList::AccountDetailsEventHandler(this,
				&frmMain::AccountDetailsEventHandler);
			moAccounts->AccountUpdate += gcnew AccountList::AccountUpdateEventHandler(this,
				&frmMain::AccountUpdateEventHandler);
			moAccounts->PositionUpdate += gcnew AccountList::PositionUpdateEventHandler(this,
				&frmMain::PositionUpdateEventHandler);

			//Display the account list.
			for each(Account ^poAccount in moAccounts)
			{
				Diagnostics::Debug::WriteLine("Account: " + poAccount->Description);
				
				//Subscribe to the account.
				poAccount->Subscribe();
			}
		}

		//Event handler for event that is raised when details for an account have 
		//changed, or a new account is recieved.
		void AccountDetailsEventHandler(AccountList::UpdateList ^poAccounts)
		{
			for each(Account ^poAccount in poAccounts)
			{
				Diagnostics::Debug::WriteLine("Account: " + poAccount->Description);
				
				////Subscribe to the account.
				poAccount->Subscribe();
			}
		}			
	
		//Event handler for event that is raised when the accounts overall balance,
		//P&L or margin details have changed.
		void AccountUpdateEventHandler(AccountList::UpdateList ^poAccounts)
		{
			//Display the account balance.
			for each(Account ^poAccount in poAccounts)

				Diagnostics::Debug::WriteLine("Account: " + poAccount->Description +
					", Balance: " + poAccount->Balance);
		}

		//Event handler for event that is raised when positions for accounts have
		//changed.
		void PositionUpdateEventHandler( AccountList::PositionUpdateList ^poPositions)
		{
			//Display the position details.
			for each(AccountList::PositionUpdateList::PositionUpdate ^poUpdate in poPositions)

				Diagnostics::Debug::WriteLine("Account: " + poUpdate->Account->Description +
					", Market: " + poUpdate->Position->Market->Description +
					", Net: " +poUpdate->Position->Net);
		}

#pragma endregion

#pragma region Single Order

		//Method that submits a single order.
		void SubmitSingleOrder()
		{
			//Get an account to submit the order for.
			Account ^poAccount = moAccounts[0];

			//Get a market to submit the order in.
			Market ^poMarket = moFilter[0];

			//Submit an order.
			double limitPrice = 107000;
			moOrder = moAccounts->SubmitNewOrder( poAccount, poMarket, BuySell::Buy,
				PriceType::Limit, TimeType::Normal, 1, limitPrice);

			//Hook Events - C++ code
			if (moOrder != nullptr)
			{
				moOrder->OrderUpdate += gcnew Order::OrderUpdateEventHandler(this,
					&frmMain::OrderUpdateEventHandler);
				moOrder->OrderFill += gcnew Order::OrderFillEventHandler(this,
					&frmMain::OrderFillEventHandler);
			}

		}
    
		//Revise the single order that was submitted.
		void ReviseSingleOrder()
		{
			//Check to see that we have an order.
			if (moOrder != nullptr)
			{
				//Check to see if the order is working.
				if (moOrder->IsWorking )
					//Revise the order.
					moOrder->ReviseTicks(2, moOrder->CurrentLimitTicks);
			}
		}

		//Pull the single order that was submitted.
		void PullSingleOrder()
		{
			//Check to see that we have an order.
			if (moOrder != nullptr)
			{
				//Check to see if the order is working.
				if (moOrder->IsWorking)
					//Pull the order.
					moOrder->Pull();
			}
		}

		//Event handler for event raised when the order has been updated.
		void OrderUpdateEventHandler(Order ^poOrder)
		{
			//Display some of the detais.
			Diagnostics::Debug::WriteLine("OrderUpdate: " + poOrder->UniqueID +
				", Status: " + poOrder->Status.ToString() + ", " + poOrder->StatusDetail +
				", Change: " + poOrder->Change.ToString() + ", Price" +
				poOrder->Market->ConvertTicksDisplay(poOrder->CurrentLimitTicks));
		}

		//Event handler for event raised when the order has received a fill.
		void OrderFillEventHandler(Order ^poOrder, Order::Trade ^poTrade)
		{
			//Display some of the detais.
			Diagnostics::Debug::WriteLine("OrderFill: " + poOrder->UniqueID +
				", Fill: " + poTrade->Volume + "@" + 
				poOrder->Market->ConvertTicksDisplay(poTrade->Ticks));
		}
		
		
#pragma endregion

#pragma region Batch Orders

		//Method that submits a batch of orders.
		void SubmitBatchOrders()
		{

			//Get an account to submit the order for.
			Account ^poAccount = moAccounts[0];

			//Get a market to submit the order in.
			Market ^poMarket = moFilter[0];

			//Create the batch submission object.
			OrderList::Submission ^poBatch = moAccounts->SubmitOrders(poAccount, poMarket);

			double limitPrice = 107000;
			double limitPrice2 = 107010;

			//Add an order to the batch.
			moOrder1 = poBatch->Add(BuySell::Buy, PriceType::Limit, 
				TimeType::Normal,	1,limitPrice);

			//Add an order to the batch.
			moOrder2 = poBatch->Add(BuySell::Buy, PriceType::Limit, 
				TimeType::Normal,	1,limitPrice2);

			//Submit the batch.
			poBatch->Submit();
		}


		//Method that revises a batch or orders.
		void ReviseBatchOrders()
		{
			 //Check to see that we have some orders.
			if (moOrder1 != nullptr && moOrder2 != nullptr)
			{
				if (moOrder1->IsWorking && moOrder2->IsWorking)
				{
					//Create the batch revision object.
					OrderList::Revision ^poBatch = moAccounts->ReviseOrders(
						moOrder1->Account, moOrder1->Market);

					//Add the order to the revision.
					poBatch->AddTicks(moOrder1, 2, moOrder1->CurrentLimitTicks);

					//Add the order to the revision.
					poBatch->AddTicks(moOrder2, 2, moOrder2->CurrentLimitTicks);

					//Revise the batch.
					poBatch->Revise();
				}
			}
		}

		//Method that pulls a batch of orders.
		void PullBatchOrders()
		{
			//Check to see that we have some orders.
			if (moOrder1 != nullptr && moOrder2 != nullptr)
			{
				if (moOrder1->IsWorking && moOrder2->IsWorking)
				{
					//Create the batch pull object.
					//OrderList::Pull ^poBatch = moAccounts->PullOrders(
					//	moOrder1->Account, moOrder1->Market);

					//Add the order to the batch.
					//poBatch->Add(moOrder1);

					//Add the order to the batch.
					//poBatch->Add(moOrder2);
				
					//Pull the batch.
					//bool bVal = poBatch->Pull();
					
					//C++ note:: a member function with the same name as the class is a constructor
					//Pull is name of the class and also the name of member function bool Pull()
					//which causes error C2273: 'function-style cast' : illegal as right side of '->' operator
					
					//This simulates batch pull
					moOrder1->Pull();
					moOrder2->Pull();

				}
			}
		}
       

#pragma endregion

#pragma region UI Handlers
	private: System::Void cmdSingleSubmit_Click(System::Object^  sender, System::EventArgs^  e) {
				SubmitSingleOrder();
			 }
	private: System::Void cmdSingleRevision_Click(System::Object^  sender, System::EventArgs^  e) {
				ReviseSingleOrder();
			 }
	private: System::Void cmdSinglePull_Click(System::Object^  sender, System::EventArgs^  e) {
				 PullSingleOrder();
		 }
	private: System::Void cmdBatchSubmit_Click(System::Object^  sender, System::EventArgs^  e) {
				 SubmitBatchOrders();
		 }
	private: System::Void cmdBatchRevision_Click(System::Object^  sender, System::EventArgs^  e) {
				 ReviseBatchOrders();
		 }
	private: System::Void cmdBatchPull_Click(System::Object^  sender, System::EventArgs^  e) {
				 PullBatchOrders();
		 }

	private: System::Void frmMain_Load(System::Object^  sender, System::EventArgs^  e) 
	{
		
		// For sim:
		moHost = Host::Login(APIServerType::Simulator, "T4Example", "112A04B0-5AAF-42F4-994E-FA7CB959C60B");
		// For live:
		//moHost = Host::Login(APIServerType::Live, "T4Example", "112A04B0-5AAF-42F4-994E-FA7CB959C60B");
		
		if (moHost != nullptr)
		{
			//We will have missed this event so call it explicitly.
			LoginSuccessEventHandler();

			// Register the host events.	
			moHost->LoginSuccess +=
				gcnew Host::LoginSuccessEventHandler(this,&frmMain::LoginSuccessEventHandler);

			moHost->LoginFailure +=
				gcnew Host::LoginFailureEventHandler(this, &frmMain::LoginFailureEventHandler);
		}
		else
			this->Close();
	}

	private: System::Void frmMain_FormClosed(System::Object^  sender, System::Windows::Forms::FormClosedEventArgs^  e) 
	{
		if (moHost != nullptr)
		{
			delete moHost;
			moHost = nullptr;
		}
	}
};


#pragma endregion
	
	

	
}

