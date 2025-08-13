CREATE PROCEDURE [dbo].[sp_Get_Dashboard_Data]
@FK_User_Id INT=NULL,

@User_Id NVARCHAR(MAX)=NULL,
@Given_Help DECIMAL(18,2)=NULL,
@Reacived_Help DECIMAL(18,2)=NULL,
@Community DECIMAL(18,2)=NULL,
@Direct_Sponser DECIMAL(18,2)=NULL,
@Pending_Reward DECIMAL(18,2)=NULL,
@Disbursed_Reward DECIMAL(18,2)=NULL,
@Delevier_Token_Id DECIMAL(18,2)=NULL,
@Pending_Token_Id DECIMAL(18,2)=NULL

AS
    IF(@FK_User_Id=1 OR @FK_User_Id=2)
	BEGIN
	    SET @User_Id ='YF123456'
	    --SELECT @Given_Help = Registration_Fee FROM tbl_Registration
	    SELECT @Reacived_Help=Registration_Fee FROM tbl_Registration
	    SELECT @Reacived_Help=Registration_Fee FROM tbl_Registration
	    SELECT @Community=COUNT(*) FROM tbl_Registration
	    SELECT @Direct_Sponser=SUM(Commission_Amount) FROM tbl_Direct_Sponsor_Comm
	    SELECT @Pending_Reward=COUNT(*) FROM tbl_User_Reward where FK_Status_Id=1 
	    SELECT @Disbursed_Reward=COUNT(*) FROM tbl_User_Reward where FK_Status_Id=2 
	    SELECT @Disbursed_Reward=COUNT(*) FROM tbl_User_Reward where FK_Status_Id=2
        SELECT @Delevier_Token_Id=COUNT(*) FROM tbl_User_Token_Request_Details UTR FULL JOIN tbl_User_Token_Transfer_Details UTT 
        ON UTT.Transfer_Token_Id=UTR.Given_Token_Id
	   
	    SELECT @Pending_Token_Id=COUNT(*) FROM tbl_User_Token_Request_Details UTR FULL JOIN tbl_User_Token_Transfer_Details UTT 
        ON UTT.Transfer_Token_Id=UTR.Given_Token_Id WHERE UTR.Is_Registered=0 OR UTT.Is_Registered=0
	    
	    SELECT @User_Id as User_Id,@Given_Help as Given_Help ,@Reacived_Help as Reacived_Help,@Community as Community,
	    ISNULL(@Direct_Sponser,0) as Direct_Sponser,@Pending_Reward as Pending_Reward,@Disbursed_Reward as Disbursed_Reward,@Delevier_Token_Id as Delevier_Token_Id,@Pending_Token_Id as Pending_Token_Id
	END
	ELSE
	BEGIN
	  DECLARE @Token_Id NVARCHAR(MAX)=(SELECT User_Name FROM tbl_User WHERE User_Id=@FK_User_Id);
	  WITH TeamCTE AS (
        SELECT Token_Id, 1 AS Level FROM tbl_registration WHERE Parent_Id=@Token_Id
        UNION ALL
        SELECT R.Token_Id, t.Level + 1 AS Level FROM tbl_registration R
        INNER JOIN 
        TeamCTE t ON R.Parent_Id = t.Token_Id
     )
	  SELECT @Community=COUNT(*) FROM TeamCTE

	  SELECT @Pending_Token_Id=COUNT(*) FROM
	  tbl_User_Token_Request_Details UTRD INNER JOIN tbl_User_Token_Request UTR ON UTRD.FK_User_Token_Request=UTR.User_Token_Request_Id
	  FULL JOIN tbl_User_Token_Transfer_Details UTTD ON UTTD.Transfer_Token_Id=UTRD.Given_Token_Id INNER JOIN tbl_User_Token_Transfer UTT
	  ON UTTD.FK_User_Token_Transfer_Id=UTT.User_Token_Transfer_Id
        WHERE (ISNULL(UTRD.Is_Registered,0)=0 OR ISNULL( UTTD.Is_Registered,0)=0)
	  AND (ISNULL(UTR.FK_Token_Id,@Token_Id)=@Token_Id OR ISNULL(UTT.FK_Token_Id, @Token_Id)=@Token_Id)

	  SELECT @Pending_Token_Id AS Total_Pending_Id, @Community AS Total_Team,* FROM tbl_Registration WHERE Token_Id=@Token_Id
	END
	