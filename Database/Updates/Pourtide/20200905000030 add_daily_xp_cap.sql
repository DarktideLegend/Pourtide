-- Create the daily_xp_cap table if it does not exist
CREATE TABLE IF NOT EXISTS `daily_xp_cap` (
    `Week` INT UNSIGNED NOT NULL,
    `Day` INT UNSIGNED NOT NULL,
    `DailyXp` BIGINT UNSIGNED NOT NULL, -- Using BIGINT UNSIGNED for `ulong`
    `StartTimestamp` DATETIME NOT NULL,
    `EndTimestamp` DATETIME NOT NULL,
    PRIMARY KEY (`Week`, `Day`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='Daily XP caps for players';