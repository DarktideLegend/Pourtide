DELETE FROM `weenie` WHERE `class_Id` = 606001;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (606001, 'ace606001-slayerextractorgem', 38, '2024-06-20 06:04:15') /* Gem */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (606001,   1,       2048) /* ItemType - Gem */
     , (606001,   5,         10) /* EncumbranceVal */
     , (606001,  11,        100) /* MaxStackSize */
     , (606001,  12,          1) /* StackSize */
     , (606001,  16,    2097160) /* ItemUseable - SourceContainedTargetRemote */
     , (606001,  19,          1) /* Value */
     , (606001,  53,        101) /* PlacementPosition - Resting */
     , (606001,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */
     , (606001,  94,         16) /* TargetType - Creature */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (606001,  11, True ) /* IgnoreCollisions */
     , (606001,  13, True ) /* Ethereal */
     , (606001,  14, True ) /* GravityStatus */
     , (606001,  19, True ) /* Attackable */
     , (606001,  69, False) /* IsSellable */
     , (606001,  60001, True) /* IsMorphGem */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (606001,   1, 'Slayer Extractor Gem') /* Name */
     , (606001,  16, 'Use this gem on any monster to extract its creature type and create a Slayer Morph Gem.') /* LongDesc */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (606001,   1,   33555677) /* Setup */
     , (606001,   3,  536870932) /* SoundTable */
     , (606001,   8,  100689142) /* Icon */
     , (606001,  22,  872415275) /* PhysicsEffectTable */;

/* Lifestoned Changelog:
{
  "LastModified": "2024-06-19T23:03:44.5335329-07:00",
  "ModifiedBy": "pourman",
  "Changelog": [],
  "UserChangeSummary": "checked and verified",
  "IsDone": true
}
*/
