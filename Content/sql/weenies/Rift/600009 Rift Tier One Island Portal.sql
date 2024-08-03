DELETE FROM `weenie` WHERE `class_Id` = 600009;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (600009, 'ace600009-rifttieroneislandportal', 7, '2024-08-02 06:14:38') /* Portal */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (600009,   1,      65536) /* ItemType - Portal */
     , (600009,  16,         32) /* ItemUseable - Remote */
     , (600009,  86,         40) /* MinLevel */
     , (600009,  87,         90) /* MaxLevel */
     , (600009,  93,       3084) /* PhysicsState - Ethereal, LightingOn */
     , (600009, 111,         56) /* PortalBitmask - NoNPK, NoSummon, NoRecall */
     , (600009, 133,          4) /* ShowableOnRadar - ShowAlways */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (600009,   1, True ) /* Stuck */
     , (600009,  11, False) /* IgnoreCollisions */
     , (600009,  12, True) /* ReportCollisions */
     , (600009,  13, True ) /* Ethereal */
     , (600009,  14, True) /* GravityStatus */
     , (600009,  15, True ) /* LightsStatus */
     , (600009,  88, False) /* PortalShowDestination */;

INSERT INTO `weenie_properties_float` (`object_Id`, `type`, `value`)
VALUES (600009,  12,     0.5) /* Shade */
     , (600009,  54,    0.75) /* UseRadius */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (600009,   1, 'Rift Tier One Island Portal') /* Name */
     , (600009,  14, 'A portal that sends you to a tier one island dungeon!') /* Use */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (600009,   1,   33556212) /* Setup */
     , (600009,   2,  150994947) /* MotionTable */
     , (600009,   6,   67109370) /* PaletteBase */
     , (600009,   7,  268435652) /* ClothingBase */
     , (600009,   8,  100667499) /* Icon */;

INSERT INTO `weenie_properties_position` (`object_Id`, `position_Type`, `obj_Cell_Id`, `origin_X`, `origin_Y`, `origin_Z`, `angles_W`, `angles_X`, `angles_Y`, `angles_Z`)
VALUES (600009, 2, 1108017213, 182.25948, 107.198425, -0.445, -0.932283, 0, 0, -0.361731) /* Destination */
/* @teleloc 0x420B003D [182.259476 107.198425 -0.445000] -0.932283 0.000000 0.000000 -0.361731 */;

/* Lifestoned Changelog:
{
  "LastModified": "2024-08-01T22:25:16.8140594-07:00",
  "ModifiedBy": "pourman",
  "Changelog": [],
  "UserChangeSummary": "Cache + Show Portal Dest",
  "IsDone": true
}
*/
