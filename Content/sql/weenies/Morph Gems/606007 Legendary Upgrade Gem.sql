DELETE FROM `weenie` WHERE `class_Id` = 606007;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (606007, 'ace606007-legendaryupgradegem', 38, '2024-06-20 13:13:38') /* Gem */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (606007,   1,       2048) /* ItemType - Gem */
     , (606007,   5,         10) /* EncumbranceVal */
     , (606007,  16,     524296) /* ItemUseable - SourceContainedTargetContained */
     , (606007,  18,         16) /* UiEffects - BoostStamina */
     , (606007,  19,          1) /* Value */
     , (606007,  53,        101) /* PlacementPosition - Resting */
     , (606007,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */
     , (606007,  94,      33039) /* TargetType - Jewelry, RedirectableItemEnchantmentTarget */
     , (606007, 60006,          3);

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (606007,  11, True ) /* IgnoreCollisions */
     , (606007,  13, True ) /* Ethereal */
     , (606007,  14, True ) /* GravityStatus */
     , (606007,  19, True ) /* Attackable */
     , (606007,  69, False) /* IsSellable */
     , (606007, 60001, True );

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (606007,   1, 'Legendary Upgrade Gem') /* Name */
     , (606007,  14, 'Use this gem on any weapon, caster, clothing, or armor piece to upgrade a random epic cantrip spell on the target item to a Legendary.') /* Use */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (606007,   1,   33555677) /* Setup */
     , (606007,   3,  536870932) /* SoundTable */
     , (606007,   8,  100689499) /* Icon */
     , (606007,  22,  872415275) /* PhysicsEffectTable */;

/* Lifestoned Changelog:
{
  "LastModified": "2024-06-20T06:11:45.1128829-07:00",
  "ModifiedBy": "pourman",
  "Changelog": [],
  "UserChangeSummary": "Weenie exported from ACEmulator world database using ACE.Adapter",
  "IsDone": true
}
*/
