-- Create the tables in the new database
CREATE TABLE IF NOT EXISTS xp_cap LIKE ace_shard.xp_cap;
CREATE TABLE IF NOT EXISTS character_login LIKE ace_shard.character_login;
CREATE TABLE IF NOT EXISTS pk_stats_damage LIKE ace_shard.pk_stats_damage;
CREATE TABLE IF NOT EXISTS pk_stats_kills LIKE ace_shard.pk_stats_kills;
CREATE TABLE IF NOT EXISTS pk_trophy_cooldown LIKE ace_shard.pk_trophy_cooldown;

-- Insert data into the new tables
INSERT INTO xp_cap SELECT * FROM ace_shard.xp_cap;
INSERT INTO character_login SELECT * FROM ace_shard.character_login;
INSERT INTO pk_stats_damage SELECT * FROM ace_shard.pk_stats_damage;
INSERT INTO pk_stats_kills SELECT * FROM ace_shard.pk_stats_kills;
INSERT INTO pk_trophy_cooldown SELECT * FROM ace_shard.pk_trophy_cooldown;

-- Switch back to the old database
USE ace_shard;

-- Drop the tables from the old database
DROP TABLE IF EXISTS xp_cap;
DROP TABLE IF EXISTS character_login;
DROP TABLE IF EXISTS pk_stats_damage;
DROP TABLE IF EXISTS pk_stats_kills;
DROP TABLE IF EXISTS pk_trophy_cooldown;

USE pourtide;