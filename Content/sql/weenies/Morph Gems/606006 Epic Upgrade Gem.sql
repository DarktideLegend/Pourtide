DELETE FROM `weenie` WHERE `class_Id` = 606006;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (606006, 'ace606006-epicupgradegem', 38, '2024-06-20 13:13:38') /* Gem */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (606006,   1,       2048) /* ItemType - Gem */
     , (606006,   5,         10) /* EncumbranceVal */
     , (606006,  16,     524296) /* ItemUseable - SourceContainedTargetContained */
     , (606006,  18,         32) /* UiEffects - Fire */
     , (606006,  19,          1) /* Value */
     , (606006,  53,        101) /* PlacementPosition - Resting */
     , (606006,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */
     , (606006,  94,      33039) /* TargetType - Jewelry, RedirectableItemEnchantmentTarget */
     , (606006, 60006,          2);

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (606006,  11, True ) /* IgnoreCollisions */
     , (606006,  13, True ) /* Ethereal */
     , (606006,  14, True ) /* GravityStatus */
     , (606006,  19, True ) /* Attackable */
     , (606006,  69, False) /* IsSellable */
     , (606006, 60001, True );

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (606006,   1, 'Epic Upgrade Gem') /* Name */
     , (606006,  14, 'Use this gem on any weapon, caster, clothing, or armor piece to upgrade a random major cantrip spell on the target item to an epic.') /* Use */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (606006,   1,   33555677) /* Setup */
     , (606006,   3,  536870932) /* SoundTable */
     , (606006,   8,  100689499) /* Icon */
     , (606006,  22,  872415275) /* PhysicsEffectTable */;

/* Lifestoned Changelog:
{
  "LastModified": "2024-06-20T06:10:42.3839478-07:00",
  "ModifiedBy": "pourman",
  "Changelog": [],
  "UserChangeSummary": "Weenie exported from ACEmulator world database using ACE.Adapter",
  "IsDone": true
}
*/
