DELETE FROM `weenie` WHERE `class_Id` = 41474;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (41474, 'ace41474-infusedlifemagic', 67, '2022-06-02 11:22:52') /* AugmentationDevice */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (41474,   1,        128) /* ItemType - Misc */
     , (41474,   5,         50) /* EncumbranceVal */
     , (41474,  16,          8) /* ItemUseable - Contained */
     , (41474,  19,         100) /* Value */
     , (41474,  33,          1) /* Bonded - Bonded */
     , (41474,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */
     , (41474, 114,          1) /* Attuned - Attuned */
     , (41474, 215,         31) /* AugmentationStat */;

INSERT INTO `weenie_properties_int64` (`object_Id`, `type`, `value`)
VALUES (41474,   3, 2000000000) /* AugmentationCost */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (41474,  11, True ) /* IgnoreCollisions */
     , (41474,  13, True ) /* Ethereal */
     , (41474,  14, True ) /* GravityStatus */
     , (41474,  19, True ) /* Attackable */
     , (41474,  22, True ) /* Inscribable */
     , (41474,  69, False) /* IsSellable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (41474,   1, 'Infused Life Magic') /* Name */
     , (41474,  16, 'Using this gem will remove your need to use a focus for Life Magic. This augmentation cannot be repeated.') /* LongDesc */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (41474,   1,   33554809) /* Setup */
     , (41474,   3,  536870932) /* SoundTable */
     , (41474,   8,  100686474) /* Icon */
     , (41474,  22,  872415275) /* PhysicsEffectTable */;

/* Lifestoned Changelog:
{
  "LastModified": "2022-06-02T04:19:39.513391-07:00",
  "ModifiedBy": "pourman",
  "Changelog": [
    {
      "created": "0001-01-01T00:00:00",
      "author": "CrimsonMage",
      "comment": "Upload"
    },
    {
      "created": "0001-01-01T00:00:00",
      "author": "CrimsonMage",
      "comment": "Fixed Descript"
    }
  ],
  "UserChangeSummary": "Fixed Descript",
  "IsDone": true
}
*/
