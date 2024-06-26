DELETE FROM `weenie` WHERE `class_Id` = 44879;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (44879, 'ace44879-armorlowerreductiontool', 38, '2024-05-13 21:40:25') /* Gem */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (44879,   1,       2048) /* ItemType - Gem */
     , (44879,   5,         10) /* EncumbranceVal */
     , (44879,  11,          1) /* MaxStackSize */
     , (44879,  12,          1) /* StackSize */
     , (44879,  16,     524296) /* ItemUseable - SourceContainedTargetContained */
     , (44879,  19,          1) /* Value */
     , (44879,  53,        101) /* PlacementPosition - Resting */
     , (44879,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */
     , (44879,  94,          6) /* TargetType - Vestements */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (44879,  11, True ) /* IgnoreCollisions */
     , (44879,  13, True ) /* Ethereal */
     , (44879,  14, True ) /* GravityStatus */
     , (44879,  19, True ) /* Attackable */
     , (44879,  22, True ) /* Inscribable */
     , (44879,  69, False) /* IsSellable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (44879,   1, 'Armor Lower Reduction Tool') /* Name */
     , (44879,  14, 'Use this tool on any loot generated multi-slot armor in order to reduce it to a single slot. It will still cover the same slots in appearance but only a single slot in armor coverage.') /* Use */
     , (44879,  16, 'This tool will reduce Sleeves to Bracers and Leggings to Greaves coverage.') /* LongDesc */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (44879,   1,   33555677) /* Setup */
     , (44879,   3,  536870932) /* SoundTable */
     , (44879,   8,  100692209) /* Icon */
     , (44879,  22,  872415275) /* PhysicsEffectTable */;

/* Lifestoned Changelog:
{
  "LastModified": "2024-05-13T14:36:00.2876951-07:00",
  "ModifiedBy": "pourman",
  "Changelog": [
    {
      "created": "2019-04-16T19:01:17.7083169-07:00",
      "author": "Chosenone",
      "comment": "Checked and Verified"
    }
  ],
  "UserChangeSummary": "Checked and Verified",
  "IsDone": true
}
*/
