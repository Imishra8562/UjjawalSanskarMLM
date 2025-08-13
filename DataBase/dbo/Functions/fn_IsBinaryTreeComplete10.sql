CREATE FUNCTION dbo.fn_IsBinaryTreeComplete10
(
    @RootTokenId NVARCHAR(50)
)
RETURNS BIT
AS
BEGIN
    DECLARE @Result BIT = 1,
    @LevelToCheck INT = 11;

    if((SELECT Gole_Completed FROM tbl_Registration WHERE Token_Id=@RootTokenId)=1)
    RETURN @Result;

    -- Table to hold nodes at each level
    DECLARE @CurrentLevel TABLE (Token_Id NVARCHAR(50));
    DECLARE @NextLevel TABLE (Token_Id NVARCHAR(50));
    DECLARE @Level INT = 1;

    -- Start with the root
    INSERT INTO @CurrentLevel (Token_Id) VALUES (@RootTokenId);

    WHILE @Level <= @LevelToCheck AND EXISTS (SELECT 1 FROM @CurrentLevel)
    BEGIN
        -- For levels 1-9, check that every node has exactly 2 children
        IF @Level < @LevelToCheck
        BEGIN
            IF EXISTS (
                SELECT c.Parent_Id
                FROM @CurrentLevel p
                LEFT JOIN tbl_Registration c WITH(NOLOCK) ON c.Parent_Id = p.Token_Id
                GROUP BY c.Parent_Id
                HAVING COUNT(c.Token_Id) <> 2
            )
            BEGIN
                SET @Result = 0;
                BREAK;
            END
        END

        -- Prepare next level
        DELETE FROM @NextLevel;
        INSERT INTO @NextLevel (Token_Id)
        SELECT c.Token_Id
        FROM @CurrentLevel p
        JOIN tbl_Registration c WITH(NOLOCK) ON c.Parent_Id = p.Token_Id;

        -- Move to next level
        DELETE FROM @CurrentLevel;
        INSERT INTO @CurrentLevel (Token_Id)
        SELECT Token_Id FROM @NextLevel;

        SET @Level = @Level + 1;
    END

    -- For level 10, just check that all expected nodes exist (no missing nodes)
    IF @Result = 1 AND @Level = @LevelToCheck+1
    BEGIN
        -- If any node is missing at level 10, the tree is not complete
        IF EXISTS (SELECT 1 FROM @CurrentLevel WHERE Token_Id IS NULL)
            SET @Result = 0;
    END
    RETURN @Result;
END