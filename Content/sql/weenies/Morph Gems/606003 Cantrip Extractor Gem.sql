DELETE FROM `weenie` WHERE `class_Id` = 606003;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (606003, 'ace606003-cantripextractorgem', 38, '2024-06-20 11:08:32') /* Gem */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (606003,   1,       2048) /* ItemType - Gem */
     , (606003,   5,         10) /* EncumbranceVal */
     , (606003,  16,     524296) /* ItemUseable - SourceContainedTargetContained */
     , (606003,  19,          1) /* Value */
     , (606003,  53,        101) /* PlacementPosition - Resting */
     , (606003,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */
     , (606003,  94,      35087) /* TargetType - Jewelry, Gem, RedirectableItemEnchantmentTarget */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (606003,  11, True ) /* IgnoreCollisions */
     , (606003,  13, True ) /* Ethereal */
     , (606003,  14, True ) /* GravityStatus */
     , (606003,  19, True ) /* Attackable */
     , (606003,  69, False) /* IsSellable */
     , (606003, 60001, True );

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (606003,   1, 'Cantrip Extractor Gem') /* Name */
     , (606003,  16, 'Use this gem on any gem, weapon, caster, clothing, or armor piece to extract a single random cantrip into a Cantrip Morph Gem.') /* LongDesc */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (606003,   1,   33555677) /* Setup */
     , (606003,   3,  536870932) /* SoundTable */
     , (606003,   8,  100689499) /* Icon */
     , (606003,  22,  872415275) /* PhysicsEffectTable */;

/* Lifestoned Changelog:
{
  "LastModified": "2024-06-20T04:06:34.0897038-07:00",
  "ModifiedBy": "pourman",
  "Changelog": [],
  "UserChangeSummary": "Weenie exported from ACEmulator world database using ACE.Adapter",
  "IsDone": true
}
*/
