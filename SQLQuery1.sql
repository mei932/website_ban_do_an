CREATE DATABASE sqlQuanLyBanDoAn ON 
(
	NAME = 'sqlQuanLyBanDoAn,',
	FILENAME = 'E:\sqlQuanLyBanDoAn\sqlQuanLyBanDoAn.mdf',
	SIZE = 100MB,
	MAXSIZE = UNLIMITED,
	FILEGROWTH = 10%
)

USE sqlQuanLyBanDoAn

CREATE TABLE tblRole
(
	iRoleID int IDENTITY,
	sRoleName nvarchar(50),
	CONSTRAINT PK_tblRole PRIMARY KEY (iRoleID),
)

CREATE TABLE tblUser
(
	iUserID int IDENTITY,
	sUserName nvarchar(100) not null,
	sUserPassword nvarchar(100) not null,
	iUserRoleID int DEFAULT 2,
	CONSTRAINT PK_tblUser PRIMARY KEY (iUserID),
	CONSTRAINT FK_tblUser_tblRole FOREIGN KEY (iUserRoleID) REFERENCES tblRole(iRoleID)
)

CREATE TABLE tblProductCategory 
(
	iCategoryID int IDENTITY,
	sCategoryName nvarchar(50) not null,
	CONSTRAINT PK_tblCategory PRIMARY KEY (iCategoryID)
)

CREATE TABLE tblProduct
(
	iProductID int IDENTITY,
	sProductName nvarchar(50) not null,
	iProductCategoryId int not null,
	fPrice float not null,
	sImageUrl nvarchar(Max) not null,
	CONSTRAINT PK_Product PRIMARY KEY (iProductID),
	CONSTRAINT FK_tblProduct_tblProductCategory 
		FOREIGN KEY (iProductCategoryId) REFERENCES tblProductCategory(iCategoryID) ON DELETE CASCADE
)

CREATE TABLE tblOrder
(
	iOrderID int IDENTITY,
	dOrderDate datetime not null DEFAULT GETDATE(),
	sCustomerName nvarchar(50) not null,
	sCustomerPhone nvarchar(20) not null,
	sCustomerAddress nvarchar(200) not null,
	fTotal float not null,
	CONSTRAINT PK_tblOrder PRIMARY KEY (iOrderID),
)

CREATE TABLE tblOrderDetail
(
	iDetailID int IDENTITY,
	iDetailOrderID int not null,
	iDetailProductID int not null,
	fDetailPrice float not null,
	iDetailQuantity int not null,
	--OrderiOrderID int not null,
	--ProductiProductID int not null,
	CONSTRAINT PK_tblOrderDetail PRIMARY KEY (iDetailID),
	CONSTRAINT FK_tblOrderDetail_tblOrder FOREIGN KEY (iDetailOrderID) REFERENCES tblOrder(iOrderID) ON DELETE CASCADE,
	CONSTRAINT FK_tblOrderDetail_tblProduct FOREIGN KEY (iDetailProductID) REFERENCES tblProduct(iProductID),
)

ALTER PROC spGetUserVM AS
BEGIN
	SELECT tblUser.iUserID, tblUser.sUserName, tblUser.sUserPassword, tblRole.sRoleName
	FROM tblUser, tblRole
	WHERE tblUser.iUserRoleID = tblRole.iRoleID
	ORDER BY tblUser.iUserID
END

exec spGetUserVM

ALTER PROC spGetProductVM AS
BEGIN
	SELECT tblProduct.iProductID, tblProduct.sProductName, tblProductCategory.sCategoryName, tblProduct.fPrice, tblProduct.sImageUrl
	FROM tblProductCategory, tblProduct
	WHERE tblProduct.iProductCategoryId = tblProductCategory.iCategoryID
	ORDER BY tblProduct.iProductID
END

exec spGetProductVM

select * from tblProduct


ALTER PROC spGetFeatureProduct AS
BEGIN
	SELECT TOP 4 tblProduct.iProductID, tblProduct.sProductName, tblProduct.iProductCategoryId, tblProduct.fPrice, tblProduct.sImageUrl
	FROM tblProduct
END

exec spGetFeatureProduct

CREATE PROC spGetProductByID @id int AS
BEGIN
	SELECT tblProduct.iProductID, tblProduct.sProductName, tblProductCategory.sCategoryName, tblProduct.fPrice, tblProduct.sImageUrl
	FROM tblProductCategory, tblProduct
	WHERE tblProduct.iProductID = @id AND tblProduct.iProductCategoryId = tblProductCategory.iCategoryID
END

exec spGetProductByID 5


CREATE PROC spGetOrderDetailByOrderID @id int AS
BEGIN
	SELECT tblOrderDetail.iDetailID ,tblOrderDetail.iDetailOrderID,tblOrderDetail.iDetailProductID, 
		tblProduct.sProductName, tblOrderDetail.iDetailQuantity, tblOrderDetail.fDetailPrice
	FROM tblOrderDetail, tblProduct
	WHERE tblOrderDetail.iDetailOrderID = @id AND tblOrderDetail.iDetailProductID = tblProduct.iProductID
END

exec spGetOrderDetailByOrderID 2

CREATE PROC spGetRelatedProduct @categoryID int AS
BEGIN
	SELECT TOP 4 tblProduct.iProductID, tblProduct.sProductName, tblProduct.iProductCategoryId, tblProduct.fPrice, tblProduct.sImageUrl
	FROM tblProduct
	WHERE tblProduct.iProductCategoryId = @categoryID
END

exec spGetRelatedProduct 2