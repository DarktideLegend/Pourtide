DELETE FROM `weenie` WHERE `class_Id` = 33611;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (33611, 'ace33611-shopathwardenchest', 20, '2024-03-30 06:31:13') /* Chest */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (33611,   1,        512) /* ItemType - Container */
     , (33611,   5,      14750) /* EncumbranceVal */
     , (33611,   6,        120) /* ItemsCapacity */
     , (33611,   7,         10) /* ContainersCapacity */
     , (33611,  16,         48) /* ItemUseable - ViewedRemote */
     , (33611,  19,       2500) /* Value */
     , (33611,  38,       9999) /* ResistLockpick */
     , (33611,  82,          30) /* InitGeneratedObjects */
     , (33611,  93,       1048) /* PhysicsState - ReportCollisions, IgnoreCollisions, Gravity */
     , (33611, 100,          1) /* GeneratorType - Relative */
     , (33611, 173,          0) /* AppraisalLockpickSuccessPercent */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (33611,   1, True ) /* Stuck */
     , (33611,   2, False) /* Open */
     , (33611,   3, True ) /* Locked */
     , (33611,  11, True ) /* IgnoreCollisions */
     , (33611,  12, True ) /* ReportCollisions */
     , (33611,  14, True ) /* GravityStatus */
     , (33611,  19, True ) /* Attackable */;

INSERT INTO `weenie_properties_float` (`object_Id`, `type`, `value`)
VALUES (33611,  11,       1) /* ResetInterval */
     , (33611,  41,       1) /* RegenerationInterval */
     , (33611,  43,       1) /* GeneratorRadius */
     , (33611,  54,       1) /* UseRadius */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (33611,   1, 'Sho Pathwarden Chest') /* Name */
     , (33611,  12, 'pathwardenchestkey') /* LockCode */
     , (33611,  14, 'Use this item to open it and see its contents.') /* Use */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (33611,   1,   33554556) /* Setup */
     , (33611,   2,  150994948) /* MotionTable */
     , (33611,   3,  536870945) /* SoundTable */
     , (33611,   8,  100667424) /* Icon */
     , (33611,  22,  872415275) /* PhysicsEffectTable */;

INSERT INTO `weenie_properties_generator` (`object_Id`, `probability`, `weenie_Class_Id`, `delay`, `init_Create`, `max_Create`, `when_Create`, `where_Create`, `stack_Size`, `palette_Id`, `shade`, `obj_Cell_Id`, `origin_X`, `origin_Y`, `origin_Z`, `angles_W`, `angles_X`, `angles_Y`, `angles_Z`)
VALUES (33611, -1, 41513, 0, 1, 1, 2, 8, -1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0) /* Generate Pathwarden Trinket (41513) (x1 up to max of 1) - Regenerate upon PickUp - Location to (re)Generate: Contain */
     , (33611, -1, 34257, 0, 1, 1, 2, 8, -1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0) /* Generate Asheron's Lesser Benediction (34257) (x1 up to max of 1) - Regenerate upon PickUp - Location to (re)Generate: Contain */
     , (33611, -1, 7595, 0, 1, 1, 2, 8, -1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0) /* Generate Refined Low-Grade Chorizite Ore */;


/* Lifestoned Changelog:
{
  "LastModified": "2022-06-12T07:30:07.7820057-07:00",
  "ModifiedBy": "neutropia",
  "Changelog": [
    {
      "created": "2022-06-12T07:53:26.7057181-07:00",
      "author": "neutropia",
      "comment": "Switching to Suikan Robe Version"
    }
  ],
  "UserChangeSummary": "Switching to Suikan Robe Version",
  "IsDone": true
}
*/
