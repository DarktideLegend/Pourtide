DELETE FROM `weenie` WHERE `class_Id` = 33609;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (33609, 'ace33609-aluvianpathwardenchest', 20, '2024-04-01 23:43:46') /* Chest */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (33609,   1,        512) /* ItemType - Container */
     , (33609,   5,      14750) /* EncumbranceVal */
     , (33609,   6,        120) /* ItemsCapacity */
     , (33609,   7,         30) /* ContainersCapacity */
     , (33609,  16,         48) /* ItemUseable - ViewedRemote */
     , (33609,  19,       2500) /* Value */
     , (33609,  38,       9999) /* ResistLockpick */
     , (33609,  82,         30) /* InitGeneratedObjects */
     , (33609,  93,       1048) /* PhysicsState - ReportCollisions, IgnoreCollisions, Gravity */
     , (33609, 100,          1) /* GeneratorType - Relative */
     , (33609, 173,          0) /* AppraisalLockpickSuccessPercent */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (33609,   1, True ) /* Stuck */
     , (33609,   2, False) /* Open */
     , (33609,   3, True ) /* Locked */
     , (33609,  11, True ) /* IgnoreCollisions */
     , (33609,  12, True ) /* ReportCollisions */
     , (33609,  14, True ) /* GravityStatus */
     , (33609,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_float` (`object_Id`, `type`, `value`)
VALUES (33609,  11,       1) /* ResetInterval */
     , (33609,  41,       1) /* RegenerationInterval */
     , (33609,  43,       1) /* GeneratorRadius */
     , (33609,  54,       1) /* UseRadius */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (33609,   1, 'Aluvian Pathwarden Chest') /* Name */
     , (33609,  12, 'pathwardenchestkey') /* LockCode */
     , (33609,  14, 'Use this item to open it and see its contents.') /* Use */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (33609,   1,   33554556) /* Setup */
     , (33609,   2,  150994948) /* MotionTable */
     , (33609,   3,  536870945) /* SoundTable */
     , (33609,   8,  100667424) /* Icon */
     , (33609,  22,  872415275) /* PhysicsEffectTable */;

INSERT INTO `weenie_properties_generator` (`object_Id`, `probability`, `weenie_Class_Id`, `delay`, `init_Create`, `max_Create`, `when_Create`, `where_Create`, `stack_Size`, `palette_Id`, `shade`, `obj_Cell_Id`, `origin_X`, `origin_Y`, `origin_Z`, `angles_W`, `angles_X`, `angles_Y`, `angles_Z`)
VALUES (33609, -1, 41513, 0, 1, 1, 2, 8, -1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0) /* Generate Pathwarden Trinket (41513) (x1 up to max of 1) - Regenerate upon PickUp - Location to (re)Generate: Contain */
     , (33609, -1, 34257, 0, 1, 1, 2, 8, -1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0) /* Generate Asheron's Lesser Benediction (34257) (x1 up to max of 1) - Regenerate upon PickUp - Location to (re)Generate: Contain */
     , (33609, -1, 7595, 0, 1, 1, 2, 8, -1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0) /* Generate Refined Low-Grade Chorizite Ore */;

/* Lifestoned Changelog:
{
  "LastModified": "2024-04-01T15:42:41.2517515-07:00",
  "ModifiedBy": "pourman",
  "Changelog": [
    {
      "created": "2024-03-30T04:27:13.5256681Z",
      "author": "ACE.Adapter",
      "comment": "Weenie exported from ACEmulator world database using ACE.Adapter"
    },
    {
      "created": "2024-04-01T23:42:26.6264544Z",
      "author": "ACE.Adapter",
      "comment": "Weenie exported from ACEmulator world database using ACE.Adapter"
    }
  ],
  "UserChangeSummary": "Weenie exported from ACEmulator world database using ACE.Adapter",
  "IsDone": true
}
*/
