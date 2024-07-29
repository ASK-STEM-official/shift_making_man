CREATE DATABASE  IF NOT EXISTS `19demo` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `19demo`;
-- MySQL dump 10.13  Distrib 8.0.34, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: 19demo
-- ------------------------------------------------------
-- Server version	8.0.35

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `admins`
--

DROP TABLE IF EXISTS `admins`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `admins` (
  `AdminID` int NOT NULL AUTO_INCREMENT,
  `Username` varchar(50) NOT NULL,
  `PasswordHash` varchar(255) NOT NULL,
  `FullName` varchar(100) NOT NULL,
  `Email` varchar(100) NOT NULL,
  PRIMARY KEY (`AdminID`),
  UNIQUE KEY `Username_UNIQUE` (`Username`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `admins`
--

LOCK TABLES `admins` WRITE;
/*!40000 ALTER TABLE `admins` DISABLE KEYS */;
INSERT INTO `admins` VALUES (1,'ootomonaiso','$2a$11$dpfh4cnfVCYh8WjcPalCrONjw8C0Gq7y6Y4WbUa71jA5SVf9I0tlK','大友内装粒式会社','ootomonaiso.tubusiki@gmail.com');
/*!40000 ALTER TABLE `admins` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `attendance`
--

DROP TABLE IF EXISTS `attendance`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `attendance` (
  `AttendanceID` int NOT NULL AUTO_INCREMENT,
  `StaffID` int DEFAULT NULL,
  `ShiftID` int DEFAULT NULL,
  `CheckInTime` datetime NOT NULL,
  `CheckOutTime` datetime DEFAULT NULL,
  PRIMARY KEY (`AttendanceID`),
  KEY `StaffID` (`StaffID`),
  KEY `ShiftID` (`ShiftID`),
  CONSTRAINT `attendance_ibfk_1` FOREIGN KEY (`StaffID`) REFERENCES `staff` (`StaffID`),
  CONSTRAINT `attendance_ibfk_2` FOREIGN KEY (`ShiftID`) REFERENCES `shifts` (`ShiftID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `attendance`
--

LOCK TABLES `attendance` WRITE;
/*!40000 ALTER TABLE `attendance` DISABLE KEYS */;
/*!40000 ALTER TABLE `attendance` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `shiftrequests`
--

DROP TABLE IF EXISTS `shiftrequests`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `shiftrequests` (
  `RequestID` int NOT NULL AUTO_INCREMENT,
  `StoreID` int NOT NULL,
  `StaffID` int DEFAULT NULL,
  `OriginalShiftID` int DEFAULT NULL,
  `RequestDate` date NOT NULL,
  `Status` int NOT NULL,
  `RequestedShiftDate` date DEFAULT NULL,
  `RequestedStartTime` time DEFAULT NULL,
  `RequestedEndTime` time DEFAULT NULL,
  PRIMARY KEY (`RequestID`),
  KEY `fk_shiftrequests_store` (`StoreID`),
  KEY `fk_shiftrequests_staff` (`StaffID`),
  KEY `fk_shiftrequests_shifts` (`OriginalShiftID`),
  CONSTRAINT `fk_shiftrequests_shifts` FOREIGN KEY (`OriginalShiftID`) REFERENCES `shifts` (`ShiftID`),
  CONSTRAINT `fk_shiftrequests_staff` FOREIGN KEY (`StaffID`) REFERENCES `staff` (`StaffID`),
  CONSTRAINT `fk_shiftrequests_store` FOREIGN KEY (`StoreID`) REFERENCES `store` (`StoreID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `shiftrequests`
--

LOCK TABLES `shiftrequests` WRITE;
/*!40000 ALTER TABLE `shiftrequests` DISABLE KEYS */;
/*!40000 ALTER TABLE `shiftrequests` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `shifts`
--

DROP TABLE IF EXISTS `shifts`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `shifts` (
  `ShiftID` int NOT NULL AUTO_INCREMENT,
  `StaffID` int DEFAULT NULL,
  `ShiftDate` date NOT NULL,
  `StartTime` time NOT NULL,
  `EndTime` time NOT NULL,
  `Status` int NOT NULL,
  `StoreID` int DEFAULT NULL,
  PRIMARY KEY (`ShiftID`)
) ENGINE=InnoDB AUTO_INCREMENT=769 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `shifts`
--

LOCK TABLES `shifts` WRITE;
/*!40000 ALTER TABLE `shifts` DISABLE KEYS */;
/*!40000 ALTER TABLE `shifts` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `staff`
--

DROP TABLE IF EXISTS `staff`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `staff` (
  `StaffID` int NOT NULL AUTO_INCREMENT,
  `Username` varchar(50) NOT NULL,
  `PasswordHash` varchar(255) NOT NULL,
  `FullName` varchar(100) NOT NULL,
  `Email` varchar(100) NOT NULL,
  `PhoneNumber` varchar(20) DEFAULT NULL,
  `EmploymentType` varchar(20) NOT NULL,
  PRIMARY KEY (`StaffID`)
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `staff`
--

LOCK TABLES `staff` WRITE;
/*!40000 ALTER TABLE `staff` DISABLE KEYS */;
INSERT INTO `staff` VALUES (1,'tanaka_t','ef92b778bafe771e89245b89ecbc08a44a4e166c06659911881f383d4473e94f','田中 太郎','tanaka.taro@example.com','080-1234-5678','Full-time'),(2,'yamada_h','9878d344400c00f8bab1a4ba1a3488b3ace88aea983e3d94ba1c781e09ba32bb','山田 花子','yamada.hanako@example.com','090-2345-6789','Part-time'),(3,'sato_i','a443522561b9588b2f8d5c1658a15af809420c326564af60d1afb5a87dafbf99','佐藤 一郎','sato.ichiro@example.com','070-3456-7890','Full-time'),(4,'suzuki_y','1189b9e999dd0012f85471afb1e2a823fd39b310884621710b296a975d3607db','鈴木 裕子','suzuki.yuko@example.com','080-4567-8901','Full-time'),(5,'watanabe_k','8be491e8ac281775a73acfb0dd2ca7155f8f849dc871188a13827b4e5196164e','渡辺 健太','watanabe.kenta@example.com','090-5678-9012','Part-time'),(6,'ito_m','0f353bfd6fd9cca08f7eeb0074fcbc8a4e7bf4f44b66ed46646469c0fe9ebfa5','伊藤 美咲','ito.misaki@example.com','070-6789-0123','Full-time'),(7,'kato_s','f9365f2220672735c9c7959fe8aebfb023229688a8fdac79a6ffeae39081609d','加藤 翔太','kato.shota@example.com','080-7890-1234','Full-time'),(8,'yoshida_a','c0299c31e19fa98afb0784d0e2dff64db71db00a8f6857132508454cbc0a1a0d','吉田 明美','yoshida.akemi@example.com','090-8901-2345','Part-time'),(9,'yamamoto_r','102fea8bc3a81bf885693111f928976108ebaebe31bf6f3501d5538c94254033','山本 涼介','yamamoto.ryosuke@example.com','070-9012-3456','Full-time'),(10,'nakamura_e','2aaaf9d18b1eaa98fd6cb368fff513f60f53a3ae72c756ad6806b8d0b246b292','中村 絵美','nakamura.emi@example.com','080-0123-4567','Full-time'),(11,'kobayashi_k','86e076c6f834b87fd105a5c731c580b2546047e894f7fcce9d6f4993988e1cd6','小林 健太郎','kobayashi.kentaro@example.com','090-1234-5678','Part-time'),(12,'saito_m','9cb23297b0c26694746d53c77adcd1ed2b0f08e4dd7165f6921ea3a02530a8c1','斎藤 美香','saito.mika@example.com','070-2345-6789','Full-time'),(13,'abe_c','a0ed99c360fb19addc0f1d6ec9da2357fb5c37656163af251de285a4421f4574','阿部 千夏','abe.chinatsu@example.com','080-3456-7890','Full-time'),(14,'matsumoto_h','35bcb533577d4c50e1d2135857ef8345fb65e7fbb0300d7baa5f5a938c83e5ea','松本 春香','matsumoto.haruka@example.com','090-4567-8901','Part-time'),(15,'inoue_d','5e1123f600cc4aabf166f4309dbfbf66a2e34f4b4ffca5162a3ebc16d2fce9b8','井上 大輔','inoue.daisuke@example.com','070-5678-9012','Full-time'),(16,'takahashi_n','5bc3c1eadad8acfd2bae3200d0504c7f551e39c0fa1aedc129c8b292ed0bebac','高橋 直子','takahashi.naoko@example.com','080-6789-0123','Full-time'),(17,'mori_j','1dfeb9144f75d13f85f1667df474693ee71a241daf0032b3913f47e6cdebda1c','森 純一','mori.junichi@example.com','090-7890-1234','Part-time'),(18,'hashimoto_y','cce13e7db3a5a3fe5240661df2c17043c77952d63a21da628c89d9324b35e8cb','橋本 優子','hashimoto.yuko@example.com','070-8901-2345','Full-time'),(19,'ishikawa_t','7fd37ccb7c661190de58641d93f9d50fb29d79bb50865366f95e03e2d16bb0a1','石川 拓也','ishikawa.takuya@example.com','080-9012-3456','Full-time'),(20,'kimura_s','89181d10445c42dbd9b402519e7ebffb5e3968ffcc6beed2cc58eb5d50382a37','木村 さくら','kimura.sakura@example.com','090-0123-4567','Part-time');
/*!40000 ALTER TABLE `staff` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `store`
--

DROP TABLE IF EXISTS `store`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `store` (
  `StoreID` int NOT NULL,
  `OpenTime` time NOT NULL,
  `CloseTime` time NOT NULL,
  `BusyTimeStart` time NOT NULL,
  `BusyTimeEnd` time NOT NULL,
  `NormalStaffCount` int NOT NULL,
  `BusyStaffCount` int NOT NULL,
  PRIMARY KEY (`StoreID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `store`
--

LOCK TABLES `store` WRITE;
/*!40000 ALTER TABLE `store` DISABLE KEYS */;
INSERT INTO `store` VALUES (1,'09:00:00','19:00:00','11:00:00','13:00:00',3,4),(2,'10:00:00','20:00:00','12:00:00','14:00:00',2,3);
/*!40000 ALTER TABLE `store` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-07-25 16:16:17
