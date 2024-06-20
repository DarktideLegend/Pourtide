DELETE FROM `weenie` WHERE `class_Id` = 606005;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (606005, 'ace606005-majorupgradegem', 38, '2024-06-20 13:13:38') /* Gem */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (606005,   1,       2048) /* ItemType - Gem */
     , (606005,   5,         10) /* EncumbranceVal */
     , (606005,  16,     524296) /* ItemUseable - SourceContainedTargetContained */
     , (606005,  18,        512) /* UiEffects - Bludgeoning */
     , (606005,  19,          1) /* Value */
     , (606005,  53,        101) /* PlacementPosition - Resting */
     , (606005,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */
     , (606005,  94,      33039) /* TargetType - Jewelry, RedirectableItemEnchantmentTarget */
     , (606005, 60006,          1);

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (606005,  11, True ) /* IgnoreCollisions */
     , (606005,  13, True ) /* Ethereal */
     , (606005,  14, True ) /* GravityStatus */
     , (606005,  19, True ) /* Attackable */
     , (606005,  69, False) /* IsSellable */
     , (606005, 60001, True );

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (606005,   1, 'Major Upgrade Gem') /* Name */
     , (606005,  14, 'Use this gem on any weapon, caster, clothing, or armor piece to upgrade a random minor cantrip spell on the target item to a major.') /* Use */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (606005,   1,   33555677) /* Setup */
     , (606005,   3,  536870932) /* SoundTable */
     , (606005,   8,  100689499) /* Icon */
     , (606005,  22,  872415275) /* PhysicsEffectTable */;

/* Lifestoned Changelog:
{
  "LastModified": "2024-06-20T06:09:58.776962-07:00",
  "ModifiedBy": "pourman",
  "Changelog": [],
  "UserChangeSummary": "Weenie exported from ACEmulator world database using ACE.Adapter",
  "IsDone": true
}
*/
