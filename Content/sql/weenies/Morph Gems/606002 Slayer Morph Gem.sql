DELETE FROM `weenie` WHERE `class_Id` = 606002;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (606002, 'ace606002-slayermorphgem', 38, '2024-06-20 05:52:47') /* Gem */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (606002,   1,       2048) /* ItemType - Gem */
     , (606002,   5,         10) /* EncumbranceVal */
     , (606002,  11,        100) /* MaxStackSize */
     , (606002,  12,          1) /* StackSize */
     , (606002,  16,     524296) /* ItemUseable - SourceContainedTargetContained */
     , (606002,  18,          2) /* UiEffects - Poisoned */
     , (606002,  19,          1) /* Value */
     , (606002,  53,        101) /* PlacementPosition - Resting */
     , (606002,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */
     , (606002,  94,      33025) /* TargetType - WeaponOrCaster */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (606002,  11, True ) /* IgnoreCollisions */
     , (606002,  13, True ) /* Ethereal */
     , (606002,  14, True ) /* GravityStatus */
     , (606002,  19, True ) /* Attackable */
     , (606002,  69, False) /* IsSellable */
     , (606002,  60001, True) /* IsMorphGem */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (606002,   1, 'Slayer Morph Gem') /* Name */
     , (606002,  14, 'Use this morph gem on any loot-generated weapon or caster to give it a Slayer effect.') /* Use */

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (606002,   1,   33555677) /* Setup */
     , (606002,   3,  536870932) /* SoundTable */
     , (606002,   8,  100689142) /* Icon */
     , (606002,  22,  872415275) /* PhysicsEffectTable */;

/* Lifestoned Changelog:
{
  "LastModified": "2024-06-19T22:33:28.6141122-07:00",
  "ModifiedBy": "pourman",
  "Changelog": [],
  "UserChangeSummary": "checked and verified",
  "IsDone": true
}
*/
