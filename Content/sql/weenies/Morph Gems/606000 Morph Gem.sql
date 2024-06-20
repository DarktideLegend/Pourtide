use ace_world;

DELETE FROM `weenie` WHERE `class_Id` = 606000;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (606000, 'ace606000-morphgem', 38, '2024-06-20 06:04:15') /* Gem */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (606000,   1,       2048) /* ItemType - Gem */
     , (606000,   5,         10) /* EncumbranceVal */
     , (606000,  11,        100) /* MaxStackSize */
     , (606000,  12,          1) /* StackSize */
     , (606000,  16,     524296) /* ItemUseable - SourceContainedTargetContained */
     , (606000,  19,          1) /* Value */
     , (606000,  53,        101) /* PlacementPosition - Resting */
     , (606000,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */
     , (606000,  94,       2054) /* TargetType - Vestements, Gem */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (606000,  11, True ) /* IgnoreCollisions */
     , (606000,  13, True ) /* Ethereal */
     , (606000,  14, True ) /* GravityStatus */
     , (606000,  19, True ) /* Attackable */
     , (606000,  69, False) /* IsSellable */
     , (606000,  60001, True) /* IsMorphGem */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (606000,   1, 'Morph Gem') /* Name */
     , (606000,  14, 'A generic morph gem that all future morph gems will inherit from.') /* Use */
     , (606000,  16, 'A generic morph gem that all future morph gems will inherit from.') /* LongDesc */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (606000,   1,   33555677) /* Setup */
     , (606000,   3,  536870932) /* SoundTable */
     , (606000,   8,  100690891) /* Icon */
     , (606000,  22,  872415275) /* PhysicsEffectTable */;

/* Lifestoned Changelog:
{
  "LastModified": "2024-06-19T19:40:48.0063536-07:00",
  "ModifiedBy": "pourman",
  "Changelog": [],
  "UserChangeSummary": "checked and verified",
  "IsDone": true
}
*/
