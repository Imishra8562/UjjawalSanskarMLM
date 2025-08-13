CREATE PROCEDURE [dbo].[sp_List_My_Team_Level] --exec sp_List_My_Team_Level @Token_Id='US123456',@Master_Id='US123456'
@Token_Id NVARCHAR(MAX)= null,
@Master_Id NVARCHAR(MAX) = null
AS
BEGIN    
 BEGIN TRY  
    WITH TeamCTE AS (
        SELECT Token_Id, 1 AS Level FROM tbl_registration WHERE Token_Id=@Token_Id
        UNION ALL
        SELECT R.Token_Id, t.Level + 1 AS Level FROM tbl_registration R
        INNER JOIN 
        TeamCTE t ON R.Parent_Id = t.Token_Id and Level<5
    )

    SELECT 
	TL.Level AS TeamLevel
    ,RE.Token_Id,
	RE.Registration_Id,
	RE.FK_Reg_Status_Id,
	RE.Full_Name,
	RE.Sponsor_Id,
	RE.Sponsor_Name,
	RE.Parent_Id,
	RE.Parent_Name,
	RE.Position,
	RE.Gender,
	S.STATUS_NAME
	,dbo.fn_IsBinaryTreeComplete10(RE.Token_Id) AS IsGoleComplete
    from tbl_Registration RE WITH(NOLOCK)
    INNER JOIN TeamCTE TC WITH(NOLOCK) ON RE.Token_Id=TC.Token_Id
	INNER JOIN tbl_User U WITH(NOLOCK) ON U.User_Id=RE.FK_User_Id
    INNER JOIN tbl_Status S WITH(NOLOCK) ON S.Status_Id=RE.FK_Reg_Status_Id
    INNER JOIN tbl_User_Role R WITH(NOLOCK) ON U.FK_User_Role_Id=R.User_Role_Id
    INNER JOIN tbl_Team_Level TL WITH(NOLOCK) ON TL.Child_Token_No= RE.Token_Id AND TL.Parent_Token_No=@Master_Id
    
    order by TC.Level
END TRY   
BEGIN CATCH    
   DECLARE @ErrorMessage VARCHAR(MAX);    
   SELECT @ErrorMessage ='SP ERROR : LIST REGISTRATION FAILED' + Char(13) + Char(10) + 'THE ERROR WAS : ' + Char(13) + Char(10) + ERROR_MESSAGE()   
   RAISERROR (@ErrorMessage, 16, 1)   
END CATCH
END
