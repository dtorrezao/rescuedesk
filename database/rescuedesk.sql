-- phpMyAdmin SQL Dump
-- version 4.7.4
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: 11-Fev-2018 às 17:29
-- Versão do servidor: 10.1.28-MariaDB
-- PHP Version: 7.1.11

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `rescuedesk`
--

-- --------------------------------------------------------

--
-- Estrutura da tabela `clientes`
--

CREATE TABLE `clientes` (
  `nrcontribuinte` int(9) NOT NULL,
  `nome` varchar(64) NOT NULL,
  `morada` varchar(128) NOT NULL,
  `codpostal` varchar(8) NOT NULL,
  `contacto` int(9) NOT NULL,
  `email` varchar(64) NOT NULL,
  `obs` text
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Extraindo dados da tabela `clientes`
--

INSERT INTO `clientes` (`nrcontribuinte`, `nome`, `morada`, `codpostal`, `contacto`, `email`, `obs`) VALUES
(135987321, 'José', 'Rua Jacinto', '2135-907', 984646684, 'cliente2@gmail.com', NULL),
(657546321, 'Manuel', 'Rua Jacinto', '2135-907', 973154645, 'cliente1@gmail.com', NULL);

-- --------------------------------------------------------

--
-- Estrutura da tabela `departamentos`
--

CREATE TABLE `departamentos` (
  `iddept` int(11) NOT NULL,
  `dept` varchar(64) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Extraindo dados da tabela `departamentos`
--

INSERT INTO `departamentos` (`iddept`, `dept`) VALUES
(1, 'Informático'),
(2, 'Comercial'),
(3, 'Adminstrativo');

-- --------------------------------------------------------

--
-- Estrutura da tabela `funcionarios`
--

CREATE TABLE `funcionarios` (
  `idfuncionario` int(11) NOT NULL,
  `nome` varchar(64) NOT NULL,
  `morada` varchar(128) NOT NULL,
  `codpostal` varchar(8) NOT NULL,
  `iddept` int(11) NOT NULL,
  `cargo` varchar(64) NOT NULL,
  `contacto` int(9) NOT NULL,
  `email` varchar(64) NOT NULL,
  `ativo` tinyint(1) NOT NULL,
  `ultlogin` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `obs` text
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Extraindo dados da tabela `funcionarios`
--

INSERT INTO `funcionarios` (`idfuncionario`, `nome`, `morada`, `codpostal`, `iddept`, `cargo`, `contacto`, `email`, `ativo`, `ultlogin`, `obs`) VALUES
(1, 'carlos', 'Rua Popular Nº22', '2135-021', 1, 'Programador', 987354726, 'funcionario1@gmail.com', 0, '2018-02-11 13:39:30', NULL),
(2, 'Ricardo', 'Avenida Central', '2120-111', 3, 'Admin', 987454741, 'adminstrador2@gmail.com', 0, '2018-02-11 14:26:26', NULL),
(3, 'David', 'Avenida Central', '2120-111', 2, 'Gestão compras', 914574541, 'funcionario2@gmail.com', 0, '2018-02-11 13:40:56', NULL),
(4, 'Ana', 'Rua Popular', '2135-021', 3, 'Admin', 987546325, 'adminstrador1@gmail.com', 0, '2018-02-11 13:59:36', NULL),
(5, 'Pedro', 'Rua Jacinto', '2135-907', 1, 'Técnico informático', 974645762, 'funcionario3@gmail.com', 0, '2018-02-11 13:59:05', NULL);

-- --------------------------------------------------------

--
-- Estrutura da tabela `localidades`
--

CREATE TABLE `localidades` (
  `codpostal` varchar(8) NOT NULL,
  `localidade` varchar(64) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Extraindo dados da tabela `localidades`
--

INSERT INTO `localidades` (`codpostal`, `localidade`) VALUES
('2120-111', 'Salvaterra de Magos'),
('2135-021', 'Porto Alto'),
('2135-907', 'Samora Correia'),
('4760-007', 'Vila Nova de Famalicão');

-- --------------------------------------------------------

--
-- Estrutura da tabela `pedidos`
--

CREATE TABLE `pedidos` (
  `idpedido` int(11) NOT NULL,
  `assunto` varchar(128) NOT NULL,
  `descricao` text NOT NULL,
  `idatividade` int(11) NOT NULL,
  `dtpedido` datetime NOT NULL,
  `dtlido` datetime NOT NULL,
  `dtmarcado` datetime NOT NULL,
  `prioridade` enum('Critica','Alta','Media','Baixa') NOT NULL,
  `dtresolvido` datetime NOT NULL,
  `nrcontribuinte` int(9) NOT NULL,
  `idfuncionario` int(11) NOT NULL,
  `obs` text
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Extraindo dados da tabela `pedidos`
--

INSERT INTO `pedidos` (`idpedido`, `assunto`, `descricao`, `idatividade`, `dtpedido`, `dtlido`, `dtmarcado`, `prioridade`, `dtresolvido`, `nrcontribuinte`, `idfuncionario`, `obs`) VALUES
(1, 'Troca Disco Rigido', 'Disco avariou', 4, '2018-02-11 13:54:35', '0000-00-00 00:00:00', '2018-02-15 13:55:39', 'Baixa', '0000-00-00 00:00:00', 657546321, 1, NULL),
(2, 'Instalação Antivirus', 'Instalaçao AVG', 3, '2018-02-11 14:23:50', '0000-00-00 00:00:00', '2018-02-12 13:56:18', 'Alta', '0000-00-00 00:00:00', 657546321, 1, NULL),
(3, 'Instalação Windows', 'Instalação de Windows 10', 1, '2018-02-11 14:25:21', '0000-00-00 00:00:00', '2018-02-13 14:25:28', 'Critica', '0000-00-00 00:00:00', 135987321, 2, NULL),
(4, 'Montagem de servidor', 'Montagem e configuração de servidor', 2, '2018-02-11 14:27:32', '0000-00-00 00:00:00', '2018-02-11 16:27:35', 'Media', '0000-00-00 00:00:00', 135987321, 2, NULL);

-- --------------------------------------------------------

--
-- Estrutura da tabela `tipoatividade`
--

CREATE TABLE `tipoatividade` (
  `idatividade` int(11) NOT NULL,
  `atividade` varchar(128) NOT NULL,
  `peso` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Extraindo dados da tabela `tipoatividade`
--

INSERT INTO `tipoatividade` (`idatividade`, `atividade`, `peso`) VALUES
(1, 'Instalação SO', 10),
(2, 'Montagem de Servidor', 30),
(3, 'Instalação de Software', 5),
(4, 'Troca de Hardware', 15);

-- --------------------------------------------------------

--
-- Estrutura da tabela `tipoutilizador`
--

CREATE TABLE `tipoutilizador` (
  `idtipo` int(11) NOT NULL,
  `tipouser` varchar(32) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Extraindo dados da tabela `tipoutilizador`
--

INSERT INTO `tipoutilizador` (`idtipo`, `tipouser`) VALUES
(1, 'Administrador'),
(2, 'Funcionário'),
(3, 'Cliente');

-- --------------------------------------------------------

--
-- Estrutura da tabela `utilizadores`
--

CREATE TABLE `utilizadores` (
  `email` varchar(64) NOT NULL,
  `password` varchar(16) NOT NULL,
  `foto` text,
  `idtipo` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Extraindo dados da tabela `utilizadores`
--

INSERT INTO `utilizadores` (`email`, `password`, `foto`, `idtipo`) VALUES
('adminstrador1@gmail.com', 'adminstrador1', NULL, 1),
('adminstrador2@gmail.com', 'adminstrador2', NULL, 1),
('cliente1@gmail.com', 'cliente1', NULL, 3),
('cliente2@gmail.com', 'cliente2', NULL, 3),
('funcionario1@gmail.com', 'funcionario1', NULL, 2),
('funcionario2@gmail.com', 'funcionario2', NULL, 2),
('funcionario3@gmail.com', 'funcionario3', NULL, 2);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `clientes`
--
ALTER TABLE `clientes`
  ADD PRIMARY KEY (`nrcontribuinte`),
  ADD KEY `codpostal` (`codpostal`),
  ADD KEY `email` (`email`);

--
-- Indexes for table `departamentos`
--
ALTER TABLE `departamentos`
  ADD PRIMARY KEY (`iddept`);

--
-- Indexes for table `funcionarios`
--
ALTER TABLE `funcionarios`
  ADD PRIMARY KEY (`idfuncionario`),
  ADD KEY `iddept` (`iddept`),
  ADD KEY `codpostal` (`codpostal`),
  ADD KEY `email` (`email`);

--
-- Indexes for table `localidades`
--
ALTER TABLE `localidades`
  ADD PRIMARY KEY (`codpostal`);

--
-- Indexes for table `pedidos`
--
ALTER TABLE `pedidos`
  ADD PRIMARY KEY (`idpedido`),
  ADD KEY `nrcontribuinte` (`nrcontribuinte`),
  ADD KEY `idfuncionario` (`idfuncionario`),
  ADD KEY `idatividade` (`idatividade`);

--
-- Indexes for table `tipoatividade`
--
ALTER TABLE `tipoatividade`
  ADD PRIMARY KEY (`idatividade`);

--
-- Indexes for table `tipoutilizador`
--
ALTER TABLE `tipoutilizador`
  ADD PRIMARY KEY (`idtipo`);

--
-- Indexes for table `utilizadores`
--
ALTER TABLE `utilizadores`
  ADD PRIMARY KEY (`email`),
  ADD KEY `tipouser` (`idtipo`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `departamentos`
--
ALTER TABLE `departamentos`
  MODIFY `iddept` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `funcionarios`
--
ALTER TABLE `funcionarios`
  MODIFY `idfuncionario` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT for table `pedidos`
--
ALTER TABLE `pedidos`
  MODIFY `idpedido` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `tipoatividade`
--
ALTER TABLE `tipoatividade`
  MODIFY `idatividade` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `tipoutilizador`
--
ALTER TABLE `tipoutilizador`
  MODIFY `idtipo` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- Constraints for dumped tables
--

--
-- Limitadores para a tabela `clientes`
--
ALTER TABLE `clientes`
  ADD CONSTRAINT `clientes_ibfk_1` FOREIGN KEY (`email`) REFERENCES `utilizadores` (`email`),
  ADD CONSTRAINT `clientes_ibfk_2` FOREIGN KEY (`codpostal`) REFERENCES `localidades` (`codpostal`);

--
-- Limitadores para a tabela `funcionarios`
--
ALTER TABLE `funcionarios`
  ADD CONSTRAINT `funcionarios_ibfk_1` FOREIGN KEY (`iddept`) REFERENCES `departamentos` (`iddept`),
  ADD CONSTRAINT `funcionarios_ibfk_2` FOREIGN KEY (`email`) REFERENCES `utilizadores` (`email`),
  ADD CONSTRAINT `funcionarios_ibfk_3` FOREIGN KEY (`codpostal`) REFERENCES `localidades` (`codpostal`);

--
-- Limitadores para a tabela `pedidos`
--
ALTER TABLE `pedidos`
  ADD CONSTRAINT `pedidos_ibfk_2` FOREIGN KEY (`idfuncionario`) REFERENCES `funcionarios` (`idfuncionario`),
  ADD CONSTRAINT `pedidos_ibfk_3` FOREIGN KEY (`nrcontribuinte`) REFERENCES `clientes` (`nrcontribuinte`),
  ADD CONSTRAINT `pedidos_ibfk_4` FOREIGN KEY (`idatividade`) REFERENCES `tipoatividade` (`idatividade`);

--
-- Limitadores para a tabela `utilizadores`
--
ALTER TABLE `utilizadores`
  ADD CONSTRAINT `utilizadores_ibfk_1` FOREIGN KEY (`idtipo`) REFERENCES `tipoutilizador` (`idtipo`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
