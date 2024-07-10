DELETE FROM `weenie` WHERE `class_Id` = 606011;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (606011, 'ace606011-rootweaponmorphgem', 38, '2024-07-09 23:12:29') /* Gem */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (606011,   1,       2048) /* ItemType - Gem */
     , (606011,   5,         10) /* EncumbranceVal */
     , (606011,  16,     524296) /* ItemUseable - SourceContainedTargetContained */
     , (606011,  18,          0) /* UiEffects - Undef */
     , (606011,  19,          1) /* Value */
     , (606011,  53,        101) /* PlacementPosition - Resting */
     , (606011,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */
     , (606011,  94,      33025) /* TargetType - WeaponOrCaster */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (606011,  11, True ) /* IgnoreCollisions */
     , (606011,  13, True ) /* Ethereal */
     , (606011,  14, True ) /* GravityStatus */
     , (606011,  19, True ) /* Attackable */
     , (606011,  69, False) /* IsSellable */
     , (606011, 60001, True );

INSERT INTO `weenie_properties_float` (`object_Id`, `type`, `value`)
VALUES (606011, 60001,       0);

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (606011,   1, 'Root Weapon Morph Gem') /* Name */
     , (606011,  14, 'Use this morph gem on any loot-generated weapon or caster to give it root capabilities.') /* Use */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (606011,   1,   33555677) /* Setup */
     , (606011,   3,  536870932) /* SoundTable */
     , (606011,   8,  100673920) /* Icon */
     , (606011,  22,  872415275) /* PhysicsEffectTable */;

/* Lifestoned Changelog:
{
  "LastModified": "2024-07-09T16:12:01.5236903-07:00",
  "ModifiedBy": "pourman",
  "Changelog": [],
  "UserChangeSummary": "Weenie exported from ACEmulator world database using ACE.Adapter",
  "IsDone": true
}
*/
