-- Keep these 'comments' here
/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;

-- Begin ACE Schema

CREATE TABLE `biota` (
  `id` int unsigned NOT NULL AUTO_INCREMENT COMMENT 'Unique Object Id within the Shard',
  `weenie_Class_Id` int unsigned NOT NULL COMMENT 'Weenie Class Id of the Weenie this Biota was created from',
  `weenie_Type` int NOT NULL DEFAULT '0' COMMENT 'WeenieType for this Object',
  `populated_Collection_Flags` int unsigned NOT NULL DEFAULT '4294967295',
  PRIMARY KEY (`id`),
  KEY `biota_wcid_idx` (`weenie_Class_Id`),
  KEY `biota_type_idx` (`weenie_Type`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='Dynamic Weenies of a Shard/World';

CREATE TABLE `biota_properties_allegiance` (
  `allegiance_Id` int unsigned NOT NULL,
  `character_Id` int unsigned NOT NULL,
  `banned` bit(1) NOT NULL,
  `approved_Vassal` bit(1) NOT NULL,
  PRIMARY KEY (`allegiance_Id`,`character_Id`),
  KEY `FK_allegiance_character_Id` (`character_Id`),
  CONSTRAINT `FK_allegiance_biota_Id` FOREIGN KEY (`allegiance_Id`) REFERENCES `biota` (`id`) ON DELETE CASCADE,
  CONSTRAINT `FK_allegiance_character_Id` FOREIGN KEY (`character_Id`) REFERENCES `character` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE `biota_properties_anim_part` (
  `id` int unsigned NOT NULL AUTO_INCREMENT COMMENT 'Unique Id of this Property',
  `object_Id` int unsigned NOT NULL DEFAULT '0' COMMENT 'Id of the object this property belongs to',
  `index` tinyint unsigned NOT NULL,
  `animation_Id` int unsigned NOT NULL,
  `order` tinyint unsigned DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `wcid_animpart_idx` (`object_Id`),
  CONSTRAINT `wcid_animpart` FOREIGN KEY (`object_Id`) REFERENCES `biota` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='Animation Part Changes (from PCAPs) of Weenies';

CREATE TABLE `biota_properties_attribute` (
  `object_Id` int unsigned NOT NULL DEFAULT '0' COMMENT 'Id of the object this property belongs to',
  `type` smallint unsigned NOT NULL DEFAULT '0' COMMENT 'Type of Property the value applies to (PropertyAttribute.????)',
  `init_Level` int unsigned NOT NULL DEFAULT '0' COMMENT 'innate points',
  `level_From_C_P` int unsigned NOT NULL DEFAULT '0' COMMENT 'points raised',
  `c_P_Spent` int unsigned NOT NULL DEFAULT '0' COMMENT 'XP spent on this attribute',
  PRIMARY KEY (`object_Id`,`type`),
  CONSTRAINT `wcid_attribute` FOREIGN KEY (`object_Id`) REFERENCES `biota` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='Attribute Properties of Weenies';

CREATE TABLE `biota_properties_attribute_2nd` (
  `object_Id` int unsigned NOT NULL DEFAULT '0' COMMENT 'Id of the object this property belongs to',
  `type` smallint unsigned NOT NULL DEFAULT '0' COMMENT 'Type of Property the value applies to (PropertyAttribute2nd.????)',
  `init_Level` int unsigned NOT NULL DEFAULT '0' COMMENT 'innate points',
  `level_From_C_P` int unsigned NOT NULL DEFAULT '0' COMMENT 'points raised',
  `c_P_Spent` int unsigned NOT NULL DEFAULT '0' COMMENT 'XP spent on this attribute',
  `current_Level` int unsigned NOT NULL DEFAULT '0' COMMENT 'current value of the vital',
  PRIMARY KEY (`object_Id`,`type`),
  CONSTRAINT `wcid_attribute2nd` FOREIGN KEY (`object_Id`) REFERENCES `biota` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='Attribute2nd (Vital) Properties of Weenies';

CREATE TABLE `biota_properties_body_part` (
  `id` int unsigned NOT NULL AUTO_INCREMENT COMMENT 'Unique Id of this Property',
  `object_Id` int unsigned NOT NULL DEFAULT '0' COMMENT 'Id of the object this property belongs to',
  `key` smallint unsigned NOT NULL DEFAULT '0' COMMENT 'Type of Property the value applies to (PropertySkill.????)',
  `d_Type` int NOT NULL DEFAULT '0',
  `d_Val` int NOT NULL DEFAULT '0',
  `d_Var` float NOT NULL DEFAULT '0',
  `base_Armor` int NOT NULL DEFAULT '0',
  `armor_Vs_Slash` int NOT NULL DEFAULT '0',
  `armor_Vs_Pierce` int NOT NULL DEFAULT '0',
  `armor_Vs_Bludgeon` int NOT NULL DEFAULT '0',
  `armor_Vs_Cold` int NOT NULL DEFAULT '0',
  `armor_Vs_Fire` int NOT NULL DEFAULT '0',
  `armor_Vs_Acid` int NOT NULL DEFAULT '0',
  `armor_Vs_Electric` int NOT NULL DEFAULT '0',
  `armor_Vs_Nether` int NOT NULL DEFAULT '0',
  `b_h` int NOT NULL DEFAULT '0',
  `h_l_f` float NOT NULL DEFAULT '0',
  `m_l_f` float NOT NULL DEFAULT '0',
  `l_l_f` float NOT NULL DEFAULT '0',
  `h_r_f` float NOT NULL DEFAULT '0',
  `m_r_f` float NOT NULL DEFAULT '0',
  `l_r_f` float NOT NULL DEFAULT '0',
  `h_l_b` float NOT NULL DEFAULT '0',
  `m_l_b` float NOT NULL DEFAULT '0',
  `l_l_b` float NOT NULL DEFAULT '0',
  `h_r_b` float NOT NULL DEFAULT '0',
  `m_r_b` float NOT NULL DEFAULT '0',
  `l_r_b` float NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`),
  UNIQUE KEY `wcid_bodypart_type_uidx` (`object_Id`,`key`),
  CONSTRAINT `wcid_bodypart` FOREIGN KEY (`object_Id`) REFERENCES `biota` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='Body Part Properties of Weenies';

CREATE TABLE `biota_properties_book` (
  `object_Id` int unsigned NOT NULL DEFAULT '0' COMMENT 'Id of the object this property belongs to',
  `max_Num_Pages` int NOT NULL DEFAULT '0' COMMENT 'Maximum number of pages per book',
  `max_Num_Chars_Per_Page` int NOT NULL DEFAULT '0' COMMENT 'Maximum number of characters per page',
  PRIMARY KEY (`object_Id`),
  CONSTRAINT `wcid_bookdata` FOREIGN KEY (`object_Id`) REFERENCES `biota` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='Book Properties of Weenies';

CREATE TABLE `biota_properties_book_page_data` (
  `id` int unsigned NOT NULL AUTO_INCREMENT COMMENT 'Unique Id of this Property',
  `object_Id` int unsigned NOT NULL DEFAULT '0' COMMENT 'Id of the Book object this page belongs to',
  `page_Id` int unsigned NOT NULL DEFAULT '0' COMMENT 'Id of the page number for this page',
  `author_Id` int unsigned NOT NULL DEFAULT '0' COMMENT 'Id of the Author of this page',
  `author_Name` varchar(255) NOT NULL DEFAULT '' COMMENT 'Character Name of the Author of this page',
  `author_Account` varchar(255) NOT NULL DEFAULT 'prewritten' COMMENT 'Account Name of the Author of this page',
  `ignore_Author` bit(1) NOT NULL COMMENT 'if this is true, any character in the world can change the page',
  `page_Text` text NOT NULL COMMENT 'Text of the Page',
  PRIMARY KEY (`id`),
  UNIQUE KEY `wcid_pageid_uidx` (`object_Id`,`page_Id`),
  CONSTRAINT `wcid_pagedata` FOREIGN KEY (`object_Id`) REFERENCES `biota` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='Page Properties of Weenies';

CREATE TABLE `biota_properties_bool` (
  `object_Id` int unsigned NOT NULL DEFAULT '0' COMMENT 'Id of the object this property belongs to',
  `type` smallint unsigned NOT NULL DEFAULT '0' COMMENT 'Type of Property the value applies to (PropertyBool.????)',
  `value` bit(1) NOT NULL COMMENT 'Value of this Property',
  PRIMARY KEY (`object_Id`,`type`),
  CONSTRAINT `wcid_bool` FOREIGN KEY (`object_Id`) REFERENCES `biota` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='Bool Properties of Weenies';


CREATE TABLE `biota_properties_create_list` (
  `id` int unsigned NOT NULL AUTO_INCREMENT COMMENT 'Unique Id of this Property',
  `object_Id` int unsigned NOT NULL DEFAULT '0' COMMENT 'Id of the object this property belongs to',
  `destination_Type` tinyint NOT NULL DEFAULT '0' COMMENT 'Type of Destination the value applies to (DestinationType.????)',
  `weenie_Class_Id` int unsigned NOT NULL DEFAULT '0' COMMENT 'Weenie Class Id of object to Create',
  `stack_Size` int NOT NULL DEFAULT '0' COMMENT 'Stack Size of object to create (-1 = infinite)',
  `palette` tinyint NOT NULL DEFAULT '0' COMMENT 'Palette Color of Object',
  `shade` float NOT NULL DEFAULT '0' COMMENT 'Shade of Object''s Palette',
  `try_To_Bond` bit(1) NOT NULL COMMENT 'Unused?',
  PRIMARY KEY (`id`),
  KEY `wcid_createlist` (`object_Id`),
  CONSTRAINT `wcid_createlist` FOREIGN KEY (`object_Id`) REFERENCES `biota` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='CreateList Properties of Weenies';

CREATE TABLE `biota_properties_d_i_d` (
  `object_Id` int unsigned NOT NULL DEFAULT '0' COMMENT 'Id of the object this property belongs to',
  `type` smallint unsigned NOT NULL DEFAULT '0' COMMENT 'Type of Property the value applies to (PropertyDataId.????)',
  `value` int unsigned NOT NULL DEFAULT '0' COMMENT 'Value of this Property',
  PRIMARY KEY (`object_Id`,`type`),
  CONSTRAINT `wcid_did` FOREIGN KEY (`object_Id`) REFERENCES `biota` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='DataID Properties of Weenies';

CREATE TABLE `biota_properties_emote` (
  `id` int unsigned NOT NULL AUTO_INCREMENT COMMENT 'Unique Id of this Property',
  `object_Id` int unsigned NOT NULL DEFAULT '0' COMMENT 'Id of the object this property belongs to',
  `category` int unsigned NOT NULL DEFAULT '0' COMMENT 'EmoteCategory',
  `probability` float NOT NULL DEFAULT '0' COMMENT 'Probability of this EmoteSet being chosen',
  `weenie_Class_Id` int unsigned DEFAULT NULL,
  `style` int unsigned DEFAULT NULL,
  `substyle` int unsigned DEFAULT NULL,
  `quest` text,
  `vendor_Type` int DEFAULT NULL,
  `min_Health` float DEFAULT NULL,
  `max_Health` float DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `wcid_emote` (`object_Id`),
  CONSTRAINT `wcid_emote` FOREIGN KEY (`object_Id`) REFERENCES `biota` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='Emote Properties of Weenies';

CREATE TABLE `biota_properties_emote_action` (
  `id` int unsigned NOT NULL AUTO_INCREMENT COMMENT 'Unique Id of this Property',
  `emote_Id` int unsigned NOT NULL DEFAULT '0' COMMENT 'Id of the emote this property belongs to',
  `order` int unsigned NOT NULL DEFAULT '0' COMMENT 'Emote Action Sequence Order',
  `type` int unsigned NOT NULL DEFAULT '0' COMMENT 'EmoteType',
  `delay` float NOT NULL DEFAULT '0' COMMENT 'Time to wait before EmoteAction starts execution',
  `extent` float NOT NULL DEFAULT '0' COMMENT '?',
  `motion` int DEFAULT NULL,
  `message` text,
  `test_String` text,
  `min` int DEFAULT NULL,
  `max` int DEFAULT NULL,
  `min_64` bigint DEFAULT NULL,
  `max_64` bigint DEFAULT NULL,
  `min_Dbl` double DEFAULT NULL,
  `max_Dbl` double DEFAULT NULL,
  `stat` int DEFAULT NULL,
  `display` bit(1) DEFAULT NULL,
  `amount` int DEFAULT NULL,
  `amount_64` bigint DEFAULT NULL,
  `hero_X_P_64` bigint DEFAULT NULL,
  `percent` double DEFAULT NULL,
  `spell_Id` int DEFAULT NULL,
  `wealth_Rating` int DEFAULT NULL,
  `treasure_Class` int DEFAULT NULL,
  `treasure_Type` int DEFAULT NULL,
  `p_Script` int DEFAULT NULL,
  `sound` int DEFAULT NULL,
  `destination_Type` tinyint DEFAULT NULL COMMENT 'Type of Destination the value applies to (DestinationType.????)',
  `weenie_Class_Id` int unsigned DEFAULT NULL COMMENT 'Weenie Class Id of object to Create',
  `stack_Size` int DEFAULT NULL COMMENT 'Stack Size of object to create (-1 = infinite)',
  `palette` int DEFAULT NULL COMMENT 'Palette Color of Object',
  `shade` float DEFAULT NULL COMMENT 'Shade of Object''s Palette',
  `try_To_Bond` bit(1) DEFAULT NULL COMMENT 'Unused?',
  `obj_Cell_Id` int unsigned DEFAULT NULL,
  `origin_X` float DEFAULT NULL,
  `origin_Y` float DEFAULT NULL,
  `origin_Z` float DEFAULT NULL,
  `angles_W` float DEFAULT NULL,
  `angles_X` float DEFAULT NULL,
  `angles_Y` float DEFAULT NULL,
  `angles_Z` float DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `wcid_category_set_order_uidx` (`emote_Id`,`order`),
  CONSTRAINT `emoteid_emoteaction` FOREIGN KEY (`emote_Id`) REFERENCES `biota_properties_emote` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='EmoteAction Properties of Weenies';

CREATE TABLE `biota_properties_enchantment_registry` (
  `object_Id` int unsigned NOT NULL DEFAULT '0' COMMENT 'Id of the object this property belongs to',
  `enchantment_Category` int unsigned NOT NULL DEFAULT '0' COMMENT 'Which PackableList this Enchantment goes in (enchantmentMask)',
  `spell_Id` int NOT NULL DEFAULT '0' COMMENT 'Id of Spell',
  `layer_Id` smallint unsigned NOT NULL DEFAULT '0' COMMENT 'Id of Layer',
  `has_Spell_Set_Id` bit(1) NOT NULL COMMENT 'Has Spell Set Id?',
  `spell_Category` smallint unsigned NOT NULL DEFAULT '0' COMMENT 'Category of Spell',
  `power_Level` int unsigned NOT NULL DEFAULT '0' COMMENT 'Power Level of Spell',
  `start_Time` double NOT NULL DEFAULT '0' COMMENT 'the amount of time this enchantment has been active',
  `duration` double NOT NULL DEFAULT '0' COMMENT 'the duration of the spell',
  `caster_Object_Id` int unsigned NOT NULL DEFAULT '0' COMMENT 'Id of the object that cast this spell',
  `degrade_Modifier` float NOT NULL DEFAULT '0' COMMENT '???',
  `degrade_Limit` float NOT NULL DEFAULT '0' COMMENT '???',
  `last_Time_Degraded` double NOT NULL DEFAULT '0' COMMENT 'the time when this enchantment was cast',
  `stat_Mod_Type` int unsigned NOT NULL DEFAULT '0' COMMENT 'flags that indicate the type of effect the spell has',
  `stat_Mod_Key` int unsigned NOT NULL DEFAULT '0' COMMENT 'along with flags, indicates which attribute is affected by the spell',
  `stat_Mod_Value` float NOT NULL DEFAULT '0' COMMENT 'the effect value/amount',
  `spell_Set_Id` int unsigned NOT NULL DEFAULT '0' COMMENT 'Id of the Spell Set for this spell',
  PRIMARY KEY (`object_Id`,`spell_Id`,`caster_Object_Id`,`layer_Id`),
  UNIQUE KEY `wcid_enchantmentregistry_objectId_spellId_layerId_uidx` (`object_Id`,`spell_Id`,`layer_Id`),
  CONSTRAINT `wcid_enchantmentregistry` FOREIGN KEY (`object_Id`) REFERENCES `biota` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='Enchantment Registry Properties of Weenies';

CREATE TABLE `biota_properties_event_filter` (
  `object_Id` int unsigned NOT NULL DEFAULT '0' COMMENT 'Id of the object this property belongs to',
  `event` int NOT NULL DEFAULT '0' COMMENT 'Id of Event to filter',
  PRIMARY KEY (`object_Id`,`event`),
  CONSTRAINT `wcid_eventfilter` FOREIGN KEY (`object_Id`) REFERENCES `biota` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='EventFilter Properties of Weenies';

CREATE TABLE `biota_properties_float` (
  `object_Id` int unsigned NOT NULL DEFAULT '0' COMMENT 'Id of the object this property belongs to',
  `type` smallint unsigned NOT NULL DEFAULT '0' COMMENT 'Type of Property the value applies to (PropertyFloat.????)',
  `value` double NOT NULL DEFAULT '0' COMMENT 'Value of this Property',
  PRIMARY KEY (`object_Id`,`type`),
  CONSTRAINT `wcid_float` FOREIGN KEY (`object_Id`) REFERENCES `biota` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='Float Properties of Weenies';

CREATE TABLE `biota_properties_generator` (
  `id` int unsigned NOT NULL AUTO_INCREMENT COMMENT 'Unique Id of this Property',
  `object_Id` int unsigned NOT NULL DEFAULT '0' COMMENT 'Id of the object this property belongs to',
  `probability` float NOT NULL DEFAULT '0',
  `weenie_Class_Id` int unsigned NOT NULL DEFAULT '0' COMMENT 'Weenie Class Id of object to generate',
  `delay` float DEFAULT '0' COMMENT 'Amount of delay before generation',
  `init_Create` int NOT NULL DEFAULT '0' COMMENT 'Number of object to generate initially',
  `max_Create` int NOT NULL DEFAULT '0' COMMENT 'Maximum amount of objects to generate',
  `when_Create` int unsigned NOT NULL DEFAULT '0' COMMENT 'When to generate the weenie object',
  `where_Create` int unsigned NOT NULL DEFAULT '0' COMMENT 'Where to generate the weenie object',
  `stack_Size` int DEFAULT NULL COMMENT 'StackSize of object generated',
  `palette_Id` int unsigned DEFAULT NULL COMMENT 'Palette Color of Object Generated',
  `shade` float DEFAULT NULL COMMENT 'Shade of Object generated''s Palette',
  `obj_Cell_Id` int unsigned DEFAULT NULL,
  `origin_X` float DEFAULT NULL,
  `origin_Y` float DEFAULT NULL,
  `origin_Z` float DEFAULT NULL,
  `angles_W` float DEFAULT NULL,
  `angles_X` float DEFAULT NULL,
  `angles_Y` float DEFAULT NULL,
  `angles_Z` float DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `wcid_generator` (`object_Id`),
  CONSTRAINT `wcid_generator` FOREIGN KEY (`object_Id`) REFERENCES `biota` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='Generator Properties of Weenies';

CREATE TABLE `biota_properties_i_i_d` (
  `object_Id` int unsigned NOT NULL DEFAULT '0' COMMENT 'Id of the object this property belongs to',
  `type` smallint unsigned NOT NULL DEFAULT '0' COMMENT 'Type of Property the value applies to (PropertyInstanceId.????)',
  `value` int unsigned NOT NULL DEFAULT '0' COMMENT 'Value of this Property',
  PRIMARY KEY (`object_Id`,`type`),
  KEY `type_value_idx` (`type`,`value`),
  CONSTRAINT `wcid_iid` FOREIGN KEY (`object_Id`) REFERENCES `biota` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='InstanceID Properties of Weenies';

CREATE TABLE `biota_properties_int` (
  `object_Id` int unsigned NOT NULL DEFAULT '0' COMMENT 'Id of the object this property belongs to',
  `type` smallint unsigned NOT NULL DEFAULT '0' COMMENT 'Type of Property the value applies to (PropertyInt.????)',
  `value` int NOT NULL DEFAULT '0' COMMENT 'Value of this Property',
  PRIMARY KEY (`object_Id`,`type`),
  CONSTRAINT `wcid_int` FOREIGN KEY (`object_Id`) REFERENCES `biota` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='Int Properties of Weenies';

CREATE TABLE `biota_properties_int64` (
  `object_Id` int unsigned NOT NULL DEFAULT '0' COMMENT 'Id of the object this property belongs to',
  `type` smallint unsigned NOT NULL DEFAULT '0' COMMENT 'Type of Property the value applies to (PropertyInt64.????)',
  `value` bigint NOT NULL DEFAULT '0' COMMENT 'Value of this Property',
  PRIMARY KEY (`object_Id`,`type`),
  CONSTRAINT `wcid_int64` FOREIGN KEY (`object_Id`) REFERENCES `biota` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='Int64 Properties of Weenies';

CREATE TABLE `biota_properties_palette` (
  `id` int unsigned NOT NULL AUTO_INCREMENT COMMENT 'Unique Id of this Property',
  `object_Id` int unsigned NOT NULL DEFAULT '0' COMMENT 'Id of the object this property belongs to',
  `sub_Palette_Id` int unsigned NOT NULL,
  `offset` smallint unsigned NOT NULL,
  `length` smallint unsigned NOT NULL,
  `order` tinyint unsigned DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `wcid_palette_idx` (`object_Id`),
  CONSTRAINT `wcid_palette` FOREIGN KEY (`object_Id`) REFERENCES `biota` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='Palette Changes (from PCAPs) of Weenies';

CREATE TABLE `biota_properties_position` (
  `object_Id` int unsigned NOT NULL COMMENT 'Id of the object this property belongs to',
  `position_Type` smallint unsigned NOT NULL COMMENT 'Type of Position the value applies to (PositionType.????)',
  `obj_Cell_Id` int unsigned NOT NULL,
  `origin_X` float NOT NULL,
  `origin_Y` float NOT NULL,
  `origin_Z` float NOT NULL,
  `angles_W` float NOT NULL,
  `angles_X` float NOT NULL,
  `angles_Y` float NOT NULL,
  `angles_Z` float NOT NULL,
  PRIMARY KEY (`object_Id`,`position_Type`),
  KEY `type_cell_idx` (`position_Type`,`obj_Cell_Id`),
  CONSTRAINT `wcid_position` FOREIGN KEY (`object_Id`) REFERENCES `biota` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='Position Properties of Weenies';

CREATE TABLE `biota_properties_skill` (
  `object_Id` int unsigned NOT NULL DEFAULT '0' COMMENT 'Id of the object this property belongs to',
  `type` smallint unsigned NOT NULL DEFAULT '0' COMMENT 'Type of Property the value applies to (PropertySkill.????)',
  `level_From_P_P` smallint unsigned NOT NULL DEFAULT '0' COMMENT 'points raised',
  `s_a_c` int unsigned NOT NULL DEFAULT '0' COMMENT 'skill state',
  `p_p` int unsigned NOT NULL DEFAULT '0' COMMENT 'XP spent on this skill',
  `init_Level` int unsigned NOT NULL DEFAULT '0' COMMENT 'starting point for advancement of the skill (eg bonus points)',
  `resistance_At_Last_Check` int unsigned NOT NULL DEFAULT '0' COMMENT 'last use difficulty',
  `last_Used_Time` double NOT NULL DEFAULT '0' COMMENT 'time skill was last used',
  PRIMARY KEY (`object_Id`,`type`),
  CONSTRAINT `wcid_skill` FOREIGN KEY (`object_Id`) REFERENCES `biota` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='Skill Properties of Weenies';

CREATE TABLE `biota_properties_spell_book` (
  `object_Id` int unsigned NOT NULL DEFAULT '0' COMMENT 'Id of the object this property belongs to',
  `spell` int NOT NULL DEFAULT '0' COMMENT 'Id of Spell',
  `probability` float NOT NULL DEFAULT '0' COMMENT 'Chance to cast this spell',
  PRIMARY KEY (`object_Id`,`spell`),
  CONSTRAINT `wcid_spellbook` FOREIGN KEY (`object_Id`) REFERENCES `biota` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='SpellBook Properties of Weenies';

CREATE TABLE `biota_properties_string` (
  `object_Id` int unsigned NOT NULL DEFAULT '0' COMMENT 'Id of the object this property belongs to',
  `type` smallint unsigned NOT NULL DEFAULT '0' COMMENT 'Type of Property the value applies to (PropertyString.????)',
  `value` text NOT NULL COMMENT 'Value of this Property',
  PRIMARY KEY (`object_Id`,`type`),
  CONSTRAINT `wcid_string` FOREIGN KEY (`object_Id`) REFERENCES `biota` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='String Properties of Weenies';

CREATE TABLE `biota_properties_texture_map` (
  `id` int unsigned NOT NULL AUTO_INCREMENT COMMENT 'Unique Id of this Property',
  `object_Id` int unsigned NOT NULL DEFAULT '0' COMMENT 'Id of the object this property belongs to',
  `index` tinyint unsigned NOT NULL,
  `old_Id` int unsigned NOT NULL,
  `new_Id` int unsigned NOT NULL,
  `order` tinyint unsigned DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `wcid_texturemap_idx` (`object_Id`),
  CONSTRAINT `wcid_texturemap` FOREIGN KEY (`object_Id`) REFERENCES `biota` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='Texture Map Changes (from PCAPs) of Weenies';

CREATE TABLE `character` (
  `id` int unsigned NOT NULL COMMENT 'Id of the Biota for this Character',
  `account_Id` int unsigned NOT NULL DEFAULT '0' COMMENT 'Id of the Biota for this Character',
  `name` varchar(255) NOT NULL COMMENT 'Name of Character',
  `is_Plussed` bit(1) NOT NULL,
  `is_Deleted` bit(1) NOT NULL COMMENT 'Is this Character deleted?',
  `delete_Time` bigint unsigned NOT NULL DEFAULT '0' COMMENT 'The character will be marked IsDeleted=True after this timestamp',
  `last_Login_Timestamp` double NOT NULL DEFAULT '0' COMMENT 'Timestamp the last time this character entered the world',
  `total_Logins` int NOT NULL DEFAULT '0',
  `character_Options_1` int NOT NULL DEFAULT '0',
  `character_Options_2` int NOT NULL DEFAULT '0',
  `gameplay_Options` blob,
  `spellbook_Filters` int unsigned NOT NULL DEFAULT '16383',
  `hair_Texture` int unsigned NOT NULL DEFAULT '0',
  `default_Hair_Texture` int unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`),
  KEY `character_account_idx` (`account_Id`),
  KEY `character_name_idx` (`name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='Int Properties of Weenies';

CREATE TABLE `character_properties_contract_registry` (
  `character_Id` int unsigned NOT NULL DEFAULT '0' COMMENT 'Id of the character this property belongs to',
  `contract_Id` int unsigned NOT NULL,
  `delete_Contract` bit(1) NOT NULL,
  `set_As_Display_Contract` bit(1) NOT NULL,
  PRIMARY KEY (`character_Id`,`contract_Id`),
  CONSTRAINT `wcid_contract` FOREIGN KEY (`character_Id`) REFERENCES `character` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE `character_properties_fill_comp_book` (
  `character_Id` int unsigned NOT NULL DEFAULT '0' COMMENT 'Id of the character this property belongs to',
  `spell_Component_Id` int NOT NULL DEFAULT '0' COMMENT 'Id of Spell Component',
  `quantity_To_Rebuy` int NOT NULL DEFAULT '0' COMMENT 'Amount of this component to add to the buy list for repurchase',
  PRIMARY KEY (`character_Id`,`spell_Component_Id`),
  CONSTRAINT `wcid_fillcompbook` FOREIGN KEY (`character_Id`) REFERENCES `character` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='FillCompBook Properties of Weenies';

CREATE TABLE `character_properties_friend_list` (
  `character_Id` int unsigned NOT NULL DEFAULT '0' COMMENT 'Id of the character this property belongs to',
  `friend_Id` int unsigned NOT NULL DEFAULT '0' COMMENT 'Id of Friend',
  PRIMARY KEY (`character_Id`,`friend_Id`),
  CONSTRAINT `wcid_friend` FOREIGN KEY (`character_Id`) REFERENCES `character` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='FriendList Properties of Weenies';

CREATE TABLE `character_properties_quest_registry` (
  `character_Id` int unsigned NOT NULL DEFAULT '0' COMMENT 'Id of the character this property belongs to',
  `quest_Name` varchar(255) NOT NULL COMMENT 'Unique Name of Quest',
  `last_Time_Completed` int unsigned NOT NULL DEFAULT '0' COMMENT 'Timestamp of last successful completion',
  `num_Times_Completed` int NOT NULL DEFAULT '0' COMMENT 'Number of successful completions',
  PRIMARY KEY (`character_Id`,`quest_Name`),
  CONSTRAINT `wcid_questbook` FOREIGN KEY (`character_Id`) REFERENCES `character` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='QuestBook Properties of Weenies';

CREATE TABLE `character_properties_shortcut_bar` (
  `character_Id` int unsigned NOT NULL DEFAULT '0' COMMENT 'Id of the character this property belongs to',
  `shortcut_Bar_Index` int unsigned NOT NULL DEFAULT '0' COMMENT 'Position (Slot) on the Shortcut Bar for this Object',
  `shortcut_Object_Id` int unsigned NOT NULL DEFAULT '0' COMMENT 'Guid of the object at this Slot',
  PRIMARY KEY (`character_Id`,`shortcut_Bar_Index`),
  KEY `wcid_shortcutbar_idx` (`character_Id`),
  CONSTRAINT `wcid_shortcutbar` FOREIGN KEY (`character_Id`) REFERENCES `character` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='ShortcutBar Properties of Weenies';

CREATE TABLE `character_properties_spell_bar` (
  `character_Id` int unsigned NOT NULL DEFAULT '0' COMMENT 'Id of the character this property belongs to',
  `spell_Bar_Number` int unsigned NOT NULL DEFAULT '0' COMMENT 'Id of Spell Bar',
  `spell_Bar_Index` int unsigned NOT NULL DEFAULT '0' COMMENT 'Position (Slot) on this Spell Bar for this Spell',
  `spell_Id` int unsigned NOT NULL DEFAULT '0' COMMENT 'Id of Spell on this Spell Bar at this Slot',
  PRIMARY KEY (`character_Id`,`spell_Bar_Number`,`spell_Id`),
  KEY `spellBar_idx` (`spell_Bar_Index`),
  CONSTRAINT `characterId_spellbar` FOREIGN KEY (`character_Id`) REFERENCES `character` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='SpellBar Properties of Weenies';

CREATE TABLE `character_properties_squelch` (
  `character_Id` int unsigned NOT NULL,
  `squelch_Character_Id` int unsigned NOT NULL,
  `squelch_Account_Id` int unsigned NOT NULL,
  `type` int unsigned NOT NULL,
  PRIMARY KEY (`character_Id`,`squelch_Character_Id`),
  CONSTRAINT `squelch_character_Id_constraint` FOREIGN KEY (`character_Id`) REFERENCES `character` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE `character_properties_title_book` (
  `character_Id` int unsigned NOT NULL DEFAULT '0' COMMENT 'Id of the character this property belongs to',
  `title_Id` int unsigned NOT NULL DEFAULT '0' COMMENT 'Id of Title',
  PRIMARY KEY (`character_Id`,`title_Id`),
  CONSTRAINT `wcid_titlebook` FOREIGN KEY (`character_Id`) REFERENCES `character` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='TitleBook Properties of Weenies';


CREATE TABLE `config_properties_boolean` (
  `key` varchar(255) NOT NULL,
  `value` bit(1) NOT NULL,
  `description` text,
  PRIMARY KEY (`key`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE `config_properties_double` (
  `key` varchar(255) NOT NULL,
  `value` double NOT NULL,
  `description` text,
  PRIMARY KEY (`key`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE `config_properties_long` (
  `key` varchar(255) NOT NULL,
  `value` bigint NOT NULL,
  `description` text,
  PRIMARY KEY (`key`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE `config_properties_string` (
  `key` varchar(255) NOT NULL,
  `value` text NOT NULL,
  `description` text,
  PRIMARY KEY (`key`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE `house_permission` (
  `house_Id` int unsigned NOT NULL COMMENT 'GUID of House Biota Object',
  `player_Guid` int unsigned NOT NULL COMMENT 'GUID of Player Biota Object being granted permission to this house',
  `storage` bit(1) NOT NULL COMMENT 'Permission includes access to House Storage',
  PRIMARY KEY (`house_Id`,`player_Guid`),
  KEY `biota_Id_house_Id_idx` (`house_Id`),
  CONSTRAINT `biota_Id_house_Id` FOREIGN KEY (`house_Id`) REFERENCES `biota` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- End ACE Schema
-- Begin ACRealms Schema

ALTER TABLE biota_properties_position ADD COLUMN instance int UNSIGNED AFTER position_Type;
-- Begin 20240414000000 MakeBiotas64Bit
ALTER TABLE biota_properties_allegiance DROP FOREIGN KEY FK_allegiance_character_Id;
ALTER TABLE character_properties_contract_registry DROP FOREIGN KEY wcid_contract;
ALTER TABLE character_properties_fill_comp_book DROP FOREIGN KEY wcid_fillcompbook;
ALTER TABLE character_properties_friend_list DROP FOREIGN KEY wcid_friend;
ALTER TABLE character_properties_quest_registry DROP FOREIGN KEY wcid_questbook;
ALTER TABLE character_properties_shortcut_bar DROP FOREIGN KEY wcid_shortcutbar;
ALTER TABLE character_properties_spell_bar DROP FOREIGN KEY characterId_spellbar;
ALTER TABLE character_properties_squelch DROP FOREIGN KEY squelch_character_Id_constraint;
ALTER TABLE character_properties_title_book DROP FOREIGN KEY wcid_titlebook;

ALTER TABLE `biota_properties_allegiance` CHANGE `character_Id` `character_Id` bigint unsigned;
ALTER TABLE `character_properties_contract_registry` CHANGE `character_Id` `character_Id` bigint unsigned NOT NULL DEFAULT '0' COMMENT 'Id of the character this property belongs to';
ALTER TABLE `character_properties_fill_comp_book` CHANGE `character_Id` `character_Id` bigint unsigned NOT NULL DEFAULT '0' COMMENT 'Id of the character this property belongs to';
ALTER TABLE `character_properties_friend_list` CHANGE `character_Id` `character_Id` bigint unsigned NOT NULL DEFAULT '0' COMMENT 'Id of the character this property belongs to';
ALTER TABLE `character_properties_quest_registry` CHANGE `character_Id` `character_Id` bigint unsigned NOT NULL DEFAULT '0' COMMENT 'Id of the character this property belongs to';
ALTER TABLE `character_properties_shortcut_bar` CHANGE `character_Id` `character_Id` bigint unsigned NOT NULL DEFAULT '0' COMMENT 'Id of the character this property belongs to';
ALTER TABLE `character_properties_spell_bar` CHANGE `character_Id` `character_Id` bigint unsigned NOT NULL DEFAULT '0' COMMENT 'Id of the character this property belongs to';
ALTER TABLE `character_properties_squelch` CHANGE `character_Id` `character_Id` bigint unsigned NOT NULL;
ALTER TABLE `character_properties_title_book` CHANGE `character_Id` `character_Id` bigint unsigned NOT NULL DEFAULT '0' COMMENT 'Id of the character this property belongs to';

ALTER TABLE `character` CHANGE `id` `id` bigint unsigned;

ALTER TABLE `biota_properties_allegiance` ADD CONSTRAINT `FK_allegiance_character_Id` FOREIGN KEY(character_Id) REFERENCES `character`(id) ON DELETE CASCADE;
ALTER TABLE `character_properties_contract_registry` ADD CONSTRAINT `wcid_contract` FOREIGN KEY(character_Id) REFERENCES `character`(id) ON DELETE CASCADE;
ALTER TABLE `character_properties_fill_comp_book` ADD CONSTRAINT `wcid_fillcompbook` FOREIGN KEY(character_Id) REFERENCES `character`(id) ON DELETE CASCADE;
ALTER TABLE `character_properties_friend_list` ADD CONSTRAINT `wcid_friend` FOREIGN KEY(character_Id) REFERENCES `character`(id) ON DELETE CASCADE;
ALTER TABLE `character_properties_quest_registry` ADD CONSTRAINT `wcid_questbook` FOREIGN KEY(character_Id) REFERENCES `character`(id) ON DELETE CASCADE;
ALTER TABLE `character_properties_shortcut_bar` ADD CONSTRAINT `wcid_shortcutbar` FOREIGN KEY(character_Id) REFERENCES `character`(id) ON DELETE CASCADE;
ALTER TABLE `character_properties_spell_bar` ADD CONSTRAINT `characterId_spellbar` FOREIGN KEY(character_Id) REFERENCES `character`(id) ON DELETE CASCADE;
ALTER TABLE `character_properties_squelch` ADD CONSTRAINT `squelch_character_Id_constraint` FOREIGN KEY(character_Id) REFERENCES `character`(id) ON DELETE CASCADE;
ALTER TABLE `character_properties_title_book` ADD CONSTRAINT `wcid_titlebook` FOREIGN KEY(character_Id) REFERENCES `character`(id) ON DELETE CASCADE;

ALTER TABLE biota_properties_allegiance DROP FOREIGN KEY FK_allegiance_biota_Id;
ALTER TABLE biota_properties_anim_part DROP FOREIGN KEY wcid_animpart;
ALTER TABLE biota_properties_attribute DROP FOREIGN KEY wcid_attribute;
ALTER TABLE biota_properties_attribute_2nd DROP FOREIGN KEY wcid_attribute2nd;
ALTER TABLE biota_properties_body_part DROP FOREIGN KEY wcid_bodypart;
ALTER TABLE biota_properties_book DROP FOREIGN KEY wcid_bookdata;
ALTER TABLE biota_properties_book_page_data DROP FOREIGN KEY wcid_pagedata;
ALTER TABLE biota_properties_bool DROP FOREIGN KEY wcid_bool;
ALTER TABLE biota_properties_create_list DROP FOREIGN KEY wcid_createlist;
ALTER TABLE biota_properties_d_i_d DROP FOREIGN KEY wcid_did;
ALTER TABLE biota_properties_emote DROP FOREIGN KEY wcid_emote;
ALTER TABLE biota_properties_enchantment_registry DROP FOREIGN KEY wcid_enchantmentregistry;
ALTER TABLE biota_properties_event_filter DROP FOREIGN KEY wcid_eventfilter;
ALTER TABLE biota_properties_float DROP FOREIGN KEY wcid_float;
ALTER TABLE biota_properties_generator DROP FOREIGN KEY wcid_generator;
ALTER TABLE biota_properties_i_i_d DROP FOREIGN KEY wcid_iid;
ALTER TABLE biota_properties_int DROP FOREIGN KEY wcid_int;
ALTER TABLE biota_properties_int64 DROP FOREIGN KEY wcid_int64;
ALTER TABLE biota_properties_palette DROP FOREIGN KEY wcid_palette;
ALTER TABLE biota_properties_position DROP FOREIGN KEY wcid_position;
ALTER TABLE biota_properties_skill DROP FOREIGN KEY wcid_skill;
ALTER TABLE biota_properties_spell_book DROP FOREIGN KEY wcid_spellbook;
ALTER TABLE biota_properties_string DROP FOREIGN KEY wcid_string;
ALTER TABLE biota_properties_texture_map DROP FOREIGN KEY wcid_texturemap;
ALTER TABLE house_permission DROP FOREIGN KEY biota_Id_house_Id;

ALTER TABLE `biota_properties_allegiance` CHANGE `allegiance_Id` `allegiance_Id` bigint unsigned;
ALTER TABLE `biota_properties_anim_part` CHANGE `object_Id` `object_Id` bigint unsigned NOT NULL DEFAULT '0' COMMENT 'Id of the object this property belongs to';
ALTER TABLE `biota_properties_attribute` CHANGE `object_Id` `object_Id` bigint unsigned NOT NULL DEFAULT '0' COMMENT 'Id of the object this property belongs to';
ALTER TABLE `biota_properties_attribute_2nd` CHANGE `object_Id` `object_Id` bigint unsigned NOT NULL DEFAULT '0' COMMENT 'Id of the object this property belongs to';
ALTER TABLE `biota_properties_body_part` CHANGE `object_Id` `object_Id` bigint unsigned NOT NULL DEFAULT '0' COMMENT 'Id of the object this property belongs to';
ALTER TABLE `biota_properties_book` CHANGE `object_Id` `object_Id` bigint unsigned NOT NULL DEFAULT '0' COMMENT 'Id of the object this property belongs to';
ALTER TABLE `biota_properties_book_page_data` CHANGE `object_Id` `object_Id` bigint unsigned NOT NULL DEFAULT '0' COMMENT 'Id of the Book object this page belongs to';
ALTER TABLE `biota_properties_bool` CHANGE `object_Id` `object_Id` bigint unsigned NOT NULL DEFAULT '0' COMMENT 'Id of the object this property belongs to';
ALTER TABLE `biota_properties_create_list` CHANGE `object_Id` `object_Id` bigint unsigned NOT NULL DEFAULT '0' COMMENT 'Id of the object this property belongs to';
ALTER TABLE `biota_properties_d_i_d` CHANGE `object_Id` `object_Id` bigint unsigned NOT NULL DEFAULT '0' COMMENT 'Id of the object this property belongs to';
ALTER TABLE `biota_properties_emote` CHANGE `object_Id` `object_Id` bigint unsigned NOT NULL DEFAULT '0' COMMENT 'Id of the object this property belongs to';
ALTER TABLE `biota_properties_enchantment_registry` CHANGE `object_Id` `object_Id` bigint unsigned NOT NULL DEFAULT '0' COMMENT 'Id of the object this property belongs to';
ALTER TABLE `biota_properties_event_filter` CHANGE `object_Id` `object_Id` bigint unsigned NOT NULL DEFAULT '0' COMMENT 'Id of the object this property belongs to';
ALTER TABLE `biota_properties_float` CHANGE `object_Id` `object_Id` bigint unsigned NOT NULL DEFAULT '0' COMMENT 'Id of the object this property belongs to';
ALTER TABLE `biota_properties_generator` CHANGE `object_Id` `object_Id` bigint unsigned NOT NULL DEFAULT '0' COMMENT 'Id of the object this property belongs to';
ALTER TABLE `biota_properties_i_i_d` CHANGE `object_Id` `object_Id` bigint unsigned NOT NULL DEFAULT '0' COMMENT 'Id of the object this property belongs to';
ALTER TABLE `biota_properties_int` CHANGE `object_Id` `object_Id` bigint unsigned NOT NULL DEFAULT '0' COMMENT 'Id of the object this property belongs to';
ALTER TABLE `biota_properties_int64` CHANGE `object_Id` `object_Id` bigint unsigned NOT NULL DEFAULT '0' COMMENT 'Id of the object this property belongs to';
ALTER TABLE `biota_properties_palette` CHANGE `object_Id` `object_Id` bigint unsigned NOT NULL DEFAULT '0' COMMENT 'Id of the object this property belongs to';
ALTER TABLE `biota_properties_position` CHANGE `object_Id` `object_Id` bigint unsigned NOT NULL COMMENT 'Id of the object this property belongs to';
ALTER TABLE `biota_properties_skill` CHANGE `object_Id` `object_Id` bigint unsigned NOT NULL DEFAULT '0' COMMENT 'Id of the object this property belongs to';
ALTER TABLE `biota_properties_spell_book` CHANGE `object_Id` `object_Id` bigint unsigned NOT NULL DEFAULT '0' COMMENT 'Id of the object this property belongs to';
ALTER TABLE `biota_properties_string` CHANGE `object_Id` `object_Id` bigint unsigned NOT NULL DEFAULT '0' COMMENT 'Id of the object this property belongs to';
ALTER TABLE `biota_properties_texture_map` CHANGE `object_Id` `object_Id` bigint unsigned NOT NULL DEFAULT '0' COMMENT 'Id of the object this property belongs to';
ALTER TABLE `house_permission` CHANGE `house_Id` `house_Id` bigint unsigned NOT NULL COMMENT 'GUID of House Biota Object';

ALTER TABLE `biota` CHANGE `id` `id` bigint unsigned;

ALTER TABLE `biota_properties_allegiance` ADD CONSTRAINT `FK_allegiance_biota_Id` FOREIGN KEY(allegiance_Id) REFERENCES biota(id) ON DELETE CASCADE;
ALTER TABLE `biota_properties_anim_part` ADD CONSTRAINT `wcid_animpart` FOREIGN KEY(object_Id) REFERENCES biota(id) ON DELETE CASCADE;
ALTER TABLE `biota_properties_attribute` ADD CONSTRAINT `wcid_attribute` FOREIGN KEY(object_Id) REFERENCES biota(id) ON DELETE CASCADE;
ALTER TABLE `biota_properties_attribute_2nd` ADD CONSTRAINT `wcid_attribute2nd` FOREIGN KEY(object_Id) REFERENCES biota(id) ON DELETE CASCADE;
ALTER TABLE `biota_properties_body_part` ADD CONSTRAINT `wcid_bodypart` FOREIGN KEY(object_Id) REFERENCES biota(id) ON DELETE CASCADE;
ALTER TABLE `biota_properties_book` ADD CONSTRAINT `wcid_bookdata` FOREIGN KEY(object_Id) REFERENCES biota(id) ON DELETE CASCADE;
ALTER TABLE `biota_properties_book_page_data` ADD CONSTRAINT `wcid_pagedata` FOREIGN KEY(object_Id) REFERENCES biota(id) ON DELETE CASCADE;
ALTER TABLE `biota_properties_bool` ADD CONSTRAINT `wcid_bool` FOREIGN KEY(object_Id) REFERENCES biota(id) ON DELETE CASCADE;
ALTER TABLE `biota_properties_create_list` ADD CONSTRAINT `wcid_createlist` FOREIGN KEY(object_Id) REFERENCES biota(id) ON DELETE CASCADE;
ALTER TABLE `biota_properties_d_i_d` ADD CONSTRAINT `wcid_did` FOREIGN KEY(object_Id) REFERENCES biota(id) ON DELETE CASCADE;
ALTER TABLE `biota_properties_emote` ADD CONSTRAINT `wcid_emote` FOREIGN KEY(object_Id) REFERENCES biota(id) ON DELETE CASCADE;
ALTER TABLE `biota_properties_enchantment_registry` ADD CONSTRAINT `wcid_enchantmentregistry` FOREIGN KEY(object_Id) REFERENCES biota(id) ON DELETE CASCADE;
ALTER TABLE `biota_properties_event_filter` ADD CONSTRAINT `wcid_eventfilter` FOREIGN KEY(object_Id) REFERENCES biota(id) ON DELETE CASCADE;
ALTER TABLE `biota_properties_float` ADD CONSTRAINT `wcid_float` FOREIGN KEY(object_Id) REFERENCES biota(id) ON DELETE CASCADE;
ALTER TABLE `biota_properties_generator` ADD CONSTRAINT `wcid_generator` FOREIGN KEY(object_Id) REFERENCES biota(id) ON DELETE CASCADE;
ALTER TABLE `biota_properties_i_i_d` ADD CONSTRAINT `wcid_iid` FOREIGN KEY(object_Id) REFERENCES biota(id) ON DELETE CASCADE;
ALTER TABLE `biota_properties_int` ADD CONSTRAINT `wcid_int` FOREIGN KEY(object_Id) REFERENCES biota(id) ON DELETE CASCADE;
ALTER TABLE `biota_properties_int64` ADD CONSTRAINT `wcid_int64` FOREIGN KEY(object_Id) REFERENCES biota(id) ON DELETE CASCADE;
ALTER TABLE `biota_properties_palette` ADD CONSTRAINT `wcid_palette` FOREIGN KEY(object_Id) REFERENCES biota(id) ON DELETE CASCADE;
ALTER TABLE `biota_properties_position` ADD CONSTRAINT `wcid_position` FOREIGN KEY(object_Id) REFERENCES biota(id) ON DELETE CASCADE;
ALTER TABLE `biota_properties_skill` ADD CONSTRAINT `wcid_skill` FOREIGN KEY(object_Id) REFERENCES biota(id) ON DELETE CASCADE;
ALTER TABLE `biota_properties_spell_book` ADD CONSTRAINT `wcid_spellbook` FOREIGN KEY(object_Id) REFERENCES biota(id) ON DELETE CASCADE;
ALTER TABLE `biota_properties_string` ADD CONSTRAINT `wcid_string` FOREIGN KEY(object_Id) REFERENCES biota(id) ON DELETE CASCADE;
ALTER TABLE `biota_properties_texture_map` ADD CONSTRAINT `wcid_texturemap` FOREIGN KEY(object_Id) REFERENCES biota(id) ON DELETE CASCADE;
ALTER TABLE `house_permission` ADD CONSTRAINT `biota_Id_house_Id` FOREIGN KEY(house_Id) REFERENCES biota(id) ON DELETE CASCADE;

ALTER TABLE `biota_properties_book_page_data` CHANGE `author_Id` `author_Id` bigint unsigned NOT NULL DEFAULT '0' COMMENT 'Id of the Author of this page';
ALTER TABLE `biota_properties_emote_action` CHANGE `Id` `Id` bigint unsigned NOT NULL AUTO_INCREMENT COMMENT 'Unique Id of this Property';
ALTER TABLE `biota_properties_enchantment_registry` CHANGE `caster_Object_Id` `caster_Object_Id` bigint unsigned NOT NULL DEFAULT '0' COMMENT 'Id of the object that cast this spell';
ALTER TABLE `biota_properties_i_i_d` CHANGE `value` `value` bigint unsigned NOT NULL DEFAULT '0' COMMENT 'Value of this Property';
ALTER TABLE `character_properties_friend_list` CHANGE `friend_Id` `friend_Id` bigint unsigned NOT NULL DEFAULT '0' COMMENT 'Id of Friend';

ALTER TABLE `character_properties_shortcut_bar` CHANGE `shortcut_Object_Id` `shortcut_Object_Id` bigint unsigned NOT NULL DEFAULT '0' COMMENT 'Guid of the object at this Slot';
ALTER TABLE `character_properties_squelch` CHANGE `squelch_Character_Id` `squelch_Character_Id` bigint unsigned NOT NULL;
ALTER TABLE `house_permission` CHANGE `player_Guid` `player_Guid` bigint unsigned NOT NULL COMMENT 'GUID of Player Biota Object being granted permission to this house';

-- End 20240414000000 MakeBiotas64Bit

-- End ACRealms Schema

/*!40101 SET character_set_client = @saved_cs_client */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;
/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;