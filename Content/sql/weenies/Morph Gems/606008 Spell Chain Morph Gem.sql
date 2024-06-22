DELETE FROM `weenie` WHERE `class_Id` = 606008;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (606008, 'ace606008-spellchainmorphgem', 38, '2024-06-21 13:14:33') /* Gem */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (606008,   1,       2048) /* ItemType - Gem */
     , (606008,   5,         10) /* EncumbranceVal */
     , (606008,  16,     524296) /* ItemUseable - SourceContainedTargetContained */
     , (606008,  18,          0) /* UiEffects - Undef */
     , (606008,  19,          1) /* Value */
     , (606008,  53,        101) /* PlacementPosition - Resting */
     , (606008,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */
     , (606008,  94,      33025) /* TargetType - WeaponOrCaster */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (606008,  11, True ) /* IgnoreCollisions */
     , (606008,  13, True ) /* Ethereal */
     , (606008,  14, True ) /* GravityStatus */
     , (606008,  19, True ) /* Attackable */
     , (606008,  69, False) /* IsSellable */
     , (606008, 60001, True );

INSERT INTO `weenie_properties_float` (`object_Id`, `type`, `value`)
VALUES (606008,   60001,       0) /* SpellChainChance */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (606008,   1, 'Spell Chain Morph Gem') /* Name */
     , (606008,  14, 'Use this morph gem on any loot-generated weapon or caster to give it a spell chain capabilities.') /* Use */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (606008,   1,   33555677) /* Setup */
     , (606008,   3,  536870932) /* SoundTable */
     , (606008,   8,  100670715) /* Icon */
     , (606008,  22,  872415275) /* PhysicsEffectTable */;

/* Lifestoned Changelog:
{
  "LastModified": "2024-06-21T06:14:27.6148373-07:00",
  "ModifiedBy": "pourman",
  "Changelog": [],
  "UserChangeSummary": "Weenie exported from ACEmulator world database using ACE.Adapter",
  "IsDone": true
}
*/
