DELETE FROM `weenie` WHERE `class_Id` = 41473;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (41473, 'ace41473-infuseditemmagic', 67, '2022-06-02 11:22:51') /* AugmentationDevice */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (41473,   1,        128) /* ItemType - Misc */
     , (41473,   5,         50) /* EncumbranceVal */
     , (41473,  16,          8) /* ItemUseable - Contained */
     , (41473,  19,         100) /* Value */
     , (41473,  33,          1) /* Bonded - Bonded */
     , (41473,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */
     , (41473, 114,          1) /* Attuned - Attuned */
     , (41473, 215,         30) /* AugmentationStat */;

INSERT INTO `weenie_properties_int64` (`object_Id`, `type`, `value`)
VALUES (41473,   3, 2000000000) /* AugmentationCost */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (41473,  11, True ) /* IgnoreCollisions */
     , (41473,  13, True ) /* Ethereal */
     , (41473,  14, True ) /* GravityStatus */
     , (41473,  19, True ) /* Attackable */
     , (41473,  22, True ) /* Inscribable */
     , (41473,  69, False) /* IsSellable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (41473,   1, 'Infused Item Magic') /* Name */
     , (41473,  16, 'Using this gem will remove your need to use a focus for Item Magic. This augmentation cannot be repeated.') /* LongDesc */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (41473,   1,   33554809) /* Setup */
     , (41473,   3,  536870932) /* SoundTable */
     , (41473,   8,  100686474) /* Icon */
     , (41473,  22,  872415275) /* PhysicsEffectTable */;

/* Lifestoned Changelog:
{
  "LastModified": "2022-06-02T04:19:28.4263142-07:00",
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
      "comment": "Fixed Descript String"
    }
  ],
  "UserChangeSummary": "Fixed Descript String",
  "IsDone": true
}
*/
