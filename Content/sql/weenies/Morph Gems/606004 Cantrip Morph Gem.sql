DELETE FROM `weenie` WHERE `class_Id` = 606004;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (606004, 'ace606004-cantripmorphgem', 38, '2024-06-20 11:08:32') /* Gem */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (606004,   1,       2048) /* ItemType - Gem */
     , (606004,   5,         10) /* EncumbranceVal */
     , (606004,  16,     524296) /* ItemUseable - SourceContainedTargetContained */
     , (606004,  18,          1) /* UiEffects - Magical */
     , (606004,  19,          1) /* Value */
     , (606004,  53,        101) /* PlacementPosition - Resting */
     , (606004,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */
     , (606004,  94,      33039) /* TargetType - Jewelry, RedirectableItemEnchantmentTarget */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (606004,  11, True ) /* IgnoreCollisions */
     , (606004,  13, True ) /* Ethereal */
     , (606004,  14, True ) /* GravityStatus */
     , (606004,  19, True ) /* Attackable */
     , (606004,  69, False) /* IsSellable */
     , (606004, 60001, True );

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (606004,   1, 'Cantrip Morph Gem') /* Name */
     , (606004,  16, 'Use this gem on any weapon, caster, clothing, or armor piece to bind the cantrip spell belonging to it.') /* LongDesc */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (606004,   1,   33555677) /* Setup */
     , (606004,   3,  536870932) /* SoundTable */
     , (606004,   8,  100689499) /* Icon */
     , (606004,  22,  872415275) /* PhysicsEffectTable */;

/* Lifestoned Changelog:
{
  "LastModified": "2024-06-20T04:07:00.2371901-07:00",
  "ModifiedBy": "pourman",
  "Changelog": [],
  "UserChangeSummary": "Weenie exported from ACEmulator world database using ACE.Adapter",
  "IsDone": true
}
*/
