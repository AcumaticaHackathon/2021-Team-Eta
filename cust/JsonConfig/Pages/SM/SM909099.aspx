<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormDetail.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="SM909099.aspx.cs" Inherits="Page_SM909099" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/FormDetail.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" Runat="Server">
	<px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%"
        TypeName="JsonConfigurator.IntegrationScenarioMaint"
        PrimaryView="Scenario"
        >
		<CallbackCommands>
            <px:PXDSCallbackCommand Name="PasteLine" Visible="False" DependOnGrid="grid" CommitChanges="true" />
            <px:PXDSCallbackCommand Name="ResetOrder" Visible="False" DependOnGrid="grid" CommitChanges="true" />
		</CallbackCommands>
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" Runat="Server">
	<px:PXFormView ID="form" runat="server" DataSourceID="ds" DataMember="Scenario" Width="100%" Height="100px" AllowAutoHide="false">
		<Template>
			<px:PXLayoutRule runat="server" StartRow="True"/>
            <px:PXSelector runat="server" ID="slScenarioID" DataField="ScenarioID" CommitChanges="True"/>
            
		</Template>
	</px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" Runat="Server">
	<px:PXGrid ID="grid" runat="server" DataSourceID="ds" Width="100%" Height="150px" SkinID="Details" AllowAutoHide="false">
		<Levels>
			<px:PXGridLevel DataMember="Detail">
			    <Columns>
                    <px:PXGridColumn DataField="Direction" CommitChanges="true"/>
                    <px:PXGridColumn DataField="WebHook" DisplayMode="Text"/>
                    <px:PXGridColumn DataField="Mapping"/>
                    <px:PXGridColumn DataField="url"/>
                    <px:PXGridColumn DataField="StatusCode"/>
			    </Columns>
			</px:PXGridLevel>
		</Levels>
		<AutoSize Container="Window" Enabled="True" MinHeight="150" />
		<ActionBar >
		</ActionBar>
        <Mode InitNewRow="True" AllowDragRows="True" ></Mode>
        <CallbackCommands PasteCommand="PasteLine">
            <Save PostData="Container" />
        </CallbackCommands>
	</px:PXGrid>
</asp:Content>
