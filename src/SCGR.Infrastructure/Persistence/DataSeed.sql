USE [SCGR];
GO

-- ========================================
-- CRIA CATEGORIAS
-- ========================================
SET IDENTITY_INSERT Categories ON;

INSERT INTO Categories (Id, Name, CreatedAt, UpdatedAt) VALUES
    (1, 'Alimentação', GETUTCDATE(), GETUTCDATE()),
    (2, 'Transporte', GETUTCDATE(), GETUTCDATE()),
    (3, 'Moradia', GETUTCDATE(), GETUTCDATE()),
    (4, 'Lazer', GETUTCDATE(), GETUTCDATE()),
    (5, 'Saúde', GETUTCDATE(), GETUTCDATE()),
    (6, 'Educação', GETUTCDATE(), GETUTCDATE()),
    (7, 'Salário', GETUTCDATE(), GETUTCDATE()),
    (8, 'Investimentos', GETUTCDATE(), GETUTCDATE());

SET IDENTITY_INSERT Categories OFF;
GO

-- ========================================
-- CRIA TRANSAÇÕES
-- ========================================
INSERT INTO Transactions (TransactionType, Description, Amount, TransactionDate, CategoryId, CreatedAt, UpdatedAt) VALUES
    (0, 'Compras no supermercado',      150.75, '2025-06-15', 1, GETUTCDATE(), GETUTCDATE()),
    (0, 'Passagem de ônibus',             4.50, '2025-06-16', 2, GETUTCDATE(), GETUTCDATE()),
    (1, 'Salário do mês',              1000.00, '2025-06-10', 7, GETUTCDATE(), GETUTCDATE()),
    (1, 'Compra de Ações',              200.00, '2025-06-20', 8, GETUTCDATE(), GETUTCDATE()),
    (0, 'Aluguel do apartamento',      1200.00, '2025-06-01', 3, GETUTCDATE(), GETUTCDATE()),
    (0, 'Cinema com amigos',             45.00, '2025-06-18', 4, GETUTCDATE(), GETUTCDATE()),
    (0, 'Consulta médica',              250.00, '2025-06-22', 5, GETUTCDATE(), GETUTCDATE()),
    (0, 'Mensalidade da faculdade',     500.00, '2025-06-05', 6, GETUTCDATE(), GETUTCDATE());
GO

-- ========================================
-- CONSULTA DADOS INSERIDOS
-- ========================================
SELECT * FROM Categories;
SELECT * FROM Transactions;
